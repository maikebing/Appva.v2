﻿// <copyright file="ContainerBuilderExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System.Collections.Generic;
    using System.Data;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Caching.Providers;
    using Appva.Core.Environment;
    using Appva.Core.Messaging.RazorMail;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Security.Middleware.Cookie;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using Appva.Mvc.Configuration;
    using Appva.Persistence;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.MultiTenant;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.Owin;
    using Microsoft.Owin.Infrastructure;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.DataHandler;
    using Microsoft.Owin.Security.DataProtection;
    using Owin;
    using RazorEngine.Configuration;
    using Appva.GrandId;
    using Appva.Http;
    using Appva.Http.ModelBinding;
    using System.Reflection;
    using System;
    using Appva.Sca;
    using System.Configuration;
    using Appva.Mcss.Admin.Application.Transformers;
    using Appva.Ehm;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ContainerBuilderExtensions
    {

        /// <summary>
        /// Registers the application environment.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        /// <remarks>This must be registered first in order</remarks>
        public static void RegisterEnvironment(this ContainerBuilder builder)
        {
            var environment = WebApplicationConfiguration.For<MvcApplication>();
            builder.Register<IApplicationEnvironment>(x => environment).SingleInstance();
        }

        /// <summary>
        /// Registers the owin context.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterOwinContext(this ContainerBuilder builder)
        {
            builder.Register(x => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
        }

        /// <summary>
        /// Registers the identity services.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterIdentityServices(this ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                return new IdentityService(x.Resolve<IOwinContext>().Environment);
            }).As<IIdentityService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers the tenant services.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterTenantServices(this ContainerBuilder builder)
        {
            builder.RegisterType<TenantService>().As<ITenantService>().SingleInstance();
            builder.RegisterType<TenantAwareMemoryCache>().As<ITenantAwareMemoryCache>().SingleInstance();
        }

        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterMediator(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers all authorization/authentication.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        /// <param name="app">The <see cref="IAppBuilder"/></param>
        public static void RegisterAuthorization(this ContainerBuilder builder, IAppBuilder app)
        {
            builder.RegisterType<FormsAuthentication>().As<IFormsAuthentication>().InstancePerRequest();
            builder.RegisterType<AuthorizeAttribute>().AsAuthorizationFilterFor<Controller>().InstancePerRequest().PropertiesAutowired();
            builder.Register(x => app).As<IAppBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<PreAuthorizeTenantMiddleware>().SingleInstance();
            builder.RegisterType<JwtSecurityTokenHandler>().AsSelf().SingleInstance();
            builder.RegisterType<JwtSecureDataFormat>().AsSelf().InstancePerRequest();
            //// Create the data protector at start up as a singleton to remove overhead on each request.
            var protector = app.CreateDataProtector(typeof(CookieAuthenticationMiddleware).FullName, AuthenticationType.Administrative.Value, "v1");
            builder.Register(x => new TicketDataFormat(protector)).AsSelf().SingleInstance();
            builder.Register(x => new CookieAuthenticationProvider()).As<ICookieAuthenticationProvider>().SingleInstance();
            builder.Register(x => new ChunkingCookieManager()).As<ICookieManager>().SingleInstance();
            //// The expiration handler will handle tenant specific expiration timeouts.
            builder.RegisterType<TenantCookieAuthenticationOptions>().InstancePerRequest();
            builder.RegisterType<TenantCookieAuthenticationMiddleware>().InstancePerRequest();
            builder.Register(x => new SecurityTokenOptions
            {
                RegisterTokenPath = new PathString("/account/register"),
                RegisterTokenExpiredPath = new PathString("/account/register/expired"),
                ResetTokenPath = new PathString("/account/reset-password"),
                ResetTokenExpiredPath = new PathString("/account/reset-password/expired"),
                TokenInvalidPath = new PathString("/auth/sign-in"),
                Provider = x.Resolve<JwtSecureDataFormat>()
            }).InstancePerRequest();
            builder.RegisterType<SecurityTokenMiddleware>().InstancePerRequest();
        }

        /// <summary>
        /// Registers the e-mail messaging.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterEmailMessaging(this ContainerBuilder builder)
        {
            builder.Register(x => new TemplateServiceConfiguration
            {
                TemplateManager = new CshtmlTemplateManager("Features/Shared/EmailTemplates")
            }).As<ITemplateServiceConfiguration>().SingleInstance();
            if (ApplicationEnvironment.Is.Production || ApplicationEnvironment.Is.Demo || ApplicationEnvironment.Is.Staging)
            {
                builder.RegisterType<MailService>().As<IRazorMailService>().SingleInstance();
            }
            else
            {
                builder.RegisterType<NoOpMailService>().As<IRazorMailService>().SingleInstance();
            }
        }

        /// <summary>
        /// Registers the nhibernate profiler.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterNhibernateProfiler(this ContainerBuilder builder)
        {
            if (ApplicationEnvironment.Is.Staging || ApplicationEnvironment.Is.Development)
            {
                //// NHibernateProfiler.Initialize();
            }
        }

        /// <summary>
        /// Registers the exception handling.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterExceptionHandling(this ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionFilter>().AsExceptionFilterFor<Controller>().InstancePerRequest();
            builder.RegisterType<MvcExceptionHandler>().As<IWebExceptionHandler>().SingleInstance();
            builder.RegisterType<PersistenceExceptionHandler>().As<IPersistenceExceptionHandler>().SingleInstance();
        }

        /// <summary>
        /// Registers the persistence.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterPersistence(this ContainerBuilder builder)
        {
            var properties = new Dictionary<string, string>();
            if (ApplicationEnvironment.Is.Production)
            {
                builder.RegisterType<ProductionTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            }
            if (ApplicationEnvironment.Is.Demo)
            {
                builder.RegisterType<DemoTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            }
            if (ApplicationEnvironment.Is.Staging)
            {
                builder.RegisterType<StagingTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
            }
            if (ApplicationEnvironment.Is.Development)
            {
                builder.RegisterType<DevelopmentTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                properties.Add("show_sql", "true");
            }
            builder.Register<RuntimeMemoryCache>(x => new RuntimeMemoryCache("https://schemas.appva.se/2015/04/cache/db/admin")).As<IRuntimeMemoryCache>().SingleInstance();
            //if (ApplicationEnvironment.Is.Development)
            //{
            //    builder.RegisterType<MockedTenantWcfClient>().As<ITenantClient>().SingleInstance();
            //}
            //else
            //{
            builder.RegisterType<TenantWcfClient>().As<ITenantClient>().SingleInstance();
            //}
            builder.Register<MultiTenantDatasourceConfiguration>(x => new MultiTenantDatasourceConfiguration
            {
                Assembly = "Appva.Mcss.Admin.Domain",
                Properties = properties
            }).As<IMultiTenantDatasourceConfiguration>().SingleInstance();
            builder.RegisterType<MultiTenantDatasource>().As<IMultiTenantDatasource>().SingleInstance();
            builder.RegisterType<MultiTenantPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance().AutoActivate();
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerRequest();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().New()).As<IPersistenceContext>().InstancePerRequest()
                .OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
        }

        /// <summary>
        /// Registers the authify client.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterGrandId(this ContainerBuilder builder)
        {
            if( ApplicationEnvironment.Is.Development )
            {
                //var modelBinder = ModelBinder.CreateNew().Bind(Assembly.GetAssembly(typeof(GrandIdClient)));
                //var options = RestOptions.CreateNew(null, modelBinder);
                //builder.Register(x => new GrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.AuthenticationServiceKey))).As<IGrandIdClient>().SingleInstance();
                //builder.Register(x => new MobileGrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.MobileAuthenticationServiceKey))).As<IMobileGrandIdClient>().SingleInstance();
                
                builder.RegisterType<MockedGrandIdClient>().As<IGrandIdClient>().InstancePerRequest();
            }
            else if (ApplicationEnvironment.Is.Staging)
            {
                var modelBinder = ModelBinder.CreateNew().Bind(Assembly.GetAssembly(typeof(GrandIdClient)));
                var options = RestOptions.CreateNew(null, modelBinder);
                builder.Register(x => new GrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.AuthenticationServiceKey))).As<IGrandIdClient>().SingleInstance();
                builder.Register(x => new MobileGrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.MobileAuthenticationServiceKey))).As<IMobileGrandIdClient>().SingleInstance();
            }
            else
            {
                var modelBinder = ModelBinder.CreateNew().Bind(Assembly.GetAssembly(typeof(GrandIdClient)));
                var options = RestOptions.CreateNew(null, modelBinder);
                builder.Register(x => new GrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.AuthenticationServiceKey))).As<IGrandIdClient>().SingleInstance();
                builder.Register(x => new MobileGrandIdClient(options, new Uri(GrandIdConfiguration.ServerUrl), GrandIdCredentials.CreateNew(GrandIdConfiguration.ApiKey, GrandIdConfiguration.MobileAuthenticationServiceKey))).As<IMobileGrandIdClient>().SingleInstance();
            }
        }

        /// <summary>
        /// Registers the tena identifi api service.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void RegisterTenaIdentifi(this ContainerBuilder builder)
        {
            var modelBinder = ModelBinder.CreateNew().Bind(Assembly.GetAssembly(typeof(TenaIdentifiClient)));
            var options     = RestOptions.CreateNew(null, modelBinder);
            builder.Register(x => new TenaIdentifiClient(options, TenaIdentifiConfiguration.ServerUrl)).As<ITenaIdentifiClient>().SingleInstance();
        }

        /// <summary>
        /// Registers all repositories.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IRepository<>).Assembly)
                .AsClosedTypesOf(typeof(IRepository<>)).AsImplementedInterfaces().InstancePerRequest();
        }

        /// <summary>
        /// Registers all services.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();
        }

        /// <summary>
        /// Registers all transformers.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/>.</param>
        public static void RegisterTransformers(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ITransformer).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(ITransformer)))).AsImplementedInterfaces().InstancePerRequest();
        }

        /// <summary>
        /// Registers the the eHM API client.
        /// </summary>
        /// <param name="builder">The current <see cref="ContainerBuilder"/></param>
        public static void RegisterEhmApi(this ContainerBuilder builder)
        {
            var configuration = new EhmConfiguration(AppvaEhmConfiguration.ServerUrl);
            var modelBinder   = ModelBinder.CreateNew().Bind(Assembly.GetAssembly(typeof(EhmClient)));
            var options       = RestOptions.CreateNew(null, modelBinder);
            builder.Register(x => new EhmClient(options, configuration)).As<IEhmClient>().SingleInstance();
        }
    }
}