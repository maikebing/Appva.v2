// <copyright file="IocConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Caching;
    using Appva.Mcss.Admin.Application.Security;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Configuration;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Siths;
    using Appva.Siths.Security;
    using Autofac;
    using Autofac.Integration.Mvc;
    using Microsoft.Owin;
    using RazorEngine.Configuration;
    using Appva.Hip;
    using Owin;

    #endregion

    /// <summary>
    /// IoC configuration.
    /// </summary>
    internal static class IocConfiguration
    {
        /// <summary>
        /// Configures IoC container. 
        /// </summary>
        public static void Configure(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            
            builder.RegisterControllers(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            //// Register repositories.
            builder.RegisterAssemblyTypes(typeof(IRepository).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IRepository)))).AsImplementedInterfaces().InstancePerRequest();

            //// Register services.
            builder.RegisterAssemblyTypes(typeof(IService).Assembly).Where(x => x.GetInterfaces()
                .Any(y => y.IsAssignableFrom(typeof(IService)))).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<TenantService>().As<ITenantService>().SingleInstance();

            //// Global tenant cache registration.
            builder.RegisterType<TenantAwareMemoryCache>().As<ITenantAwareMemoryCache>().SingleInstance();

            //// Owin registration.
            builder.Register(x => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
            builder.Register(x =>
            {
                return new IdentityService(x.Resolve<IOwinContext>().Environment);
            }).As<IIdentityService>().InstancePerLifetimeScope();

            //// Authentication registration.
            builder.RegisterType<FormsAuthentication>().As<IFormsAuthentication>().InstancePerRequest();

            //// Mediator registration.
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();

            //// SITHS.
            builder.Register<SithsClient>(x => new SithsClient(new AuthifyWtfTokenizer())).As<ISithsClient>().SingleInstance();

            //// HIP
            builder.Register<DemoHipClient>(x => new DemoHipClient()).As<IHipClient>().SingleInstance(); 

            //// Register modules.
            builder.RegisterModule(PersistenceModule.CreateNew());
            builder.RegisterModule(MessagingModule.CreateNew());
            builder.RegisterModule(ExceptionHandlerModule.CreateNew());
            
            builder.RegisterType<AuthorizeAttribute>().AsAuthorizationFilterFor<Controller>().InstancePerRequest().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            //// Register pre authorize tenant provider as the 1st middleware to execute.
            builder.RegisterType<PreAuthorizeTenantIdentityProvider>().SingleInstance();

            //// Create container and set resolvers and owin interceptors.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}