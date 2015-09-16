// <copyright file="ContainerBuilderExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Apis.TenantServer.Legacy;
    using Appva.Caching.Providers;
    using Appva.Core.Environment;
    using Appva.Core.Messaging;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using Appva.Mvc.Configuration;
    using Appva.Persistence;
    using Appva.Persistence.Autofac;
    using Appva.Persistence.MultiTenant;
    using Appva.Siths;
    using Appva.Siths.Security;
    using Appva.Tenant.Identity;
    using Appva.Tenant.Interoperability.Client;
    using Autofac;
    using Autofac.Integration.Mvc;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using RazorEngine.Configuration;
    using Mvc = Appva.Mvc.Messaging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        public static void RegisterEnvironment(this ContainerBuilder builder)
        {
            var environment = WebApplicationConfiguration.For<MvcApplication>();
            builder.Register<IApplicationEnvironment>(x => environment).SingleInstance();
        }

        public static void RegisterOwinContext(this ContainerBuilder builder)
        {
            builder.Register(x => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
        }

        public static void RegisterIdentityServices(this ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                return new IdentityService(x.Resolve<IOwinContext>().Environment);
            }).As<IIdentityService>().InstancePerLifetimeScope();
        }

        public static void RegisterTenantServices(this ContainerBuilder builder)
        {
            builder.RegisterType<TenantService>().As<ITenantService>().SingleInstance();
            builder.RegisterType<TenantAwareMemoryCache>().As<ITenantAwareMemoryCache>().SingleInstance();
        }

        public static void RegisterMediator(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static void RegisterAuthorization(this ContainerBuilder builder, IAppBuilder app)
        {
            builder.RegisterType<FormsAuthentication>().As<IFormsAuthentication>().InstancePerRequest();
            builder.RegisterType<AuthorizeAttribute>().AsAuthorizationFilterFor<Controller>().InstancePerRequest().PropertiesAutowired();
            builder.Register(x => app).As<IAppBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<PreAuthorizeTenantMiddleware>().SingleInstance();
            builder.RegisterType<JwtSecurityTokenHandler>().AsSelf().SingleInstance();
            builder.RegisterType<JwtSecureDataFormat>().AsSelf().InstancePerRequest();
            builder.Register<ResetPasswordOptions>(x => new ResetPasswordOptions
            {
                TokenQueryParam    = "token",
                ResetPasswordPath  = new PathString("/auth/reset-password"),
                TokenExpiredPath   = new PathString("/auth/reset-password/expired"),
                TokenInvalidPath   = new PathString("/auth/sign-in"),
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = OAuthDefaults.AuthenticationType,
                Provider           = x.Resolve<JwtSecureDataFormat>()
            }).InstancePerRequest();
            builder.RegisterType<ResetPasswordMiddleware>().InstancePerRequest();
            builder.Register<CookieAuthenticationOptions>(x => new CookieAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = AuthenticationType.Administrative.Value,
                CookieSecure       = CookieSecureOption.SameAsRequest,
                LoginPath          = new PathString("/auth/sign-in"),
                LogoutPath         = new PathString("/auth/sign-out"),
                SlidingExpiration  = true,
                ExpireTimeSpan     = TimeSpan.FromMinutes(15)
            }).SingleInstance();
            builder.RegisterType<CookieAuthenticationMiddleware>().SingleInstance();
        }

        public static void RegisterEmailMessaging(this ContainerBuilder builder)
        {
            builder.Register(x => new TemplateServiceConfiguration
            {
                TemplateManager = new Mvc.CshtmlTemplateManager("Features/Shared/EmailTemplates")
            }).As<ITemplateServiceConfiguration>().SingleInstance();
            if (ApplicationEnvironment.Is.Production || ApplicationEnvironment.Is.Demo || ApplicationEnvironment.Is.Staging)
            {
                builder.RegisterType<Mvc.MailService>().As<Mvc.IRazorMailService>().SingleInstance();
            }
            else
            {
                builder.RegisterType<Mvc.NoOpMailService>().As<Mvc.IRazorMailService>().SingleInstance();
            }
        }

        public static void RegisterNhibernateProvider(this ContainerBuilder builder)
        {
            if (ApplicationEnvironment.Is.Development)
            {
                NHibernateProfiler.Initialize();
            }
        }

        public static void RegisterExceptionHandling(this ContainerBuilder builder)
        {
            builder.RegisterType<ExceptionFilter>().AsExceptionFilterFor<Controller>().InstancePerRequest();
            builder.RegisterType<MvcExceptionHandler>().As<IWebExceptionHandler>().SingleInstance();
            builder.RegisterType<PersistenceExceptionHandler>().As<IPersistenceExceptionHandler>().SingleInstance();
        }

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
                properties.Add("dialect", "NHibernate.Dialect.MsSql2008Dialect");
            }
            if (ApplicationEnvironment.Is.Development)
            {
                builder.RegisterType<DevelopmentTenantIdentificationStrategy>().As<ITenantIdentificationStrategy>().SingleInstance();
                properties.Add("show_sql", "true");
            }
            builder.Register<RuntimeMemoryCache>(x => new RuntimeMemoryCache("https://schemas.appva.se/2015/04/cache/db/admin")).As<IRuntimeMemoryCache>().SingleInstance();
            builder.RegisterType<TenantWcfClient>().As<ITenantClient>().SingleInstance();
            builder.Register<MultiTenantDatasourceConfiguration>(x => new MultiTenantDatasourceConfiguration
            {
                Assembly = "Appva.Mcss.Admin.Domain",
                Properties = properties
            }).As<IMultiTenantDatasourceConfiguration>().SingleInstance();
            builder.RegisterType<MultiTenantDatasource>().As<IMultiTenantDatasource>().SingleInstance();
            builder.RegisterType<MultiTenantPersistenceContextAwareResolver>().As<IPersistenceContextAwareResolver>().SingleInstance().AutoActivate();
            builder.RegisterType<TrackablePersistenceContext>().AsSelf().InstancePerLifetimeScope();
            builder.Register(x => x.Resolve<IPersistenceContextAwareResolver>().CreateNew()).As<IPersistenceContext>().InstancePerLifetimeScope()
                .OnActivated(x => x.Context.Resolve<TrackablePersistenceContext>().Persistence.Open().BeginTransaction(IsolationLevel.ReadCommitted));
        }

        public static void RegisterAuthify(this ContainerBuilder builder)
        {
            builder.Register<SithsClient>(x => new SithsClient(new AuthifyWtfTokenizer())).As<ISithsClient>().SingleInstance();
        }

        public static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IRepository)))).AsImplementedInterfaces().InstancePerRequest();
        }

        public static void RegisterServices(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();
        }

        
    }
}