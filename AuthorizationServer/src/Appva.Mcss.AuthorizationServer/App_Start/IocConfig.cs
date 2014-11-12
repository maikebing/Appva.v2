// <copyright file="IocConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Application;
    using Appva.Mcss.AuthorizationServer.Infrastructure;
    using Appva.Mcss.AuthorizationServer.Infrastructure.WebApi;
    using Appva.OAuth;
    using Appva.Persistence.Autofac;
    using Autofac;
    using Autofac.Features.Variance;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using Core.Configuration;
    using Cqrs;
    using Domain.Services;
    using DotNetOpenAuth.Messaging.Bindings;
    using DotNetOpenAuth.OAuth2;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using Microsoft.Owin;
    using Mvc.Imaging;
    using Owin;
    using Persistence;
    using Persistence.Providers;

    #endregion

    /// <summary>
    /// Autofac configuration.
    /// </summary>
    internal static class IocConfig
    {
        /// <summary>
        /// Configures AutoFac and Persistence.
        /// </summary>
        /// <param name="app">OWIN builder</param>
        /// <param name="authenticationType">Authentication type</param>
        public static void Register(IAppBuilder app, string authenticationType)
        {
            //// NHibernateProfiler.Initialize();
            var privateKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPjkwNU9zRjVnYXNIOUVFY0VYV2RaSXNpNlozbWxKRjhlMFlPancrVmY0M0lYTnhmc3ZzOUxvdTR6dVpUOHV5dndpT25jaDUrSXBIOHZTZ2ZzaUZLbFZuQXRzcXhUcU5HVXFBWk5HWG9rZ3FiS0d6WTFoajZLVWxHUlErcThJMHdFbzBrWFh3cjQ3bWFIN01pRVYvaXBiSjZvVmtkbC9XVHJybXMyb2JFR09CRT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjxQPi9DSTYxZ1ZhZzlUMTRkMmdLb0hJSUc2NU5rQ0FzQlVzTlEzMGtRK2l2UEFIWTV5b2JpSXdxTDVxSk54cjhsVUhGMDJxQVR2TUxOYnNaT3J2a3V1bjF3PT08L1A+PFE+K3hrZ0pSQXJhRFRiem9VeElEbVZ4UVZtUVhia1NFS244bnpTYkZEN2ptSzRKd3h5NmlNR2ZTakljdFliNmhxQTc3dFlrTzFpSHVHTnZtS0FHMU9DVnc9PTwvUT48RFA+R1MwUjB1MFY3TFFIR1ZhWDk2YWQ1UjhwUDFHUmlBT1ZObmIrUkwzYThpTEZtaHk2ZE1UVk53Uk1kUUhOaFpVWDhDdkJIZjVxbE0raEt6S0tXWkZPWVE9PTwvRFA+PERRPjRQMnpldUpSTXE5aVlWdWhHREhoREVmNVJ5RmtEWWVFZTFmektGRXNCbnBZYmN6T3p4TVJSbWFicmFKQ0l2TWFvelNvZUR2c1ZxVmVYOEJjNzU5VlF3PT08L0RRPjxJbnZlcnNlUT5vVm5hNG1HQkx5SzN3OHdOQzhGVVBlVHlISzN5SkFSTXdDU0ZTLytTajI5eUdEbCtPeE5CRlNvUW9uWmwwdWFFeFdBN0VJTjJVZUxSZzhicWFELzUyQT09PC9JbnZlcnNlUT48RD5ZTkJQRGN4a2dtYWU0eGhxSlFhb1ptMmVTNVBiaW5tU1h3TGh3WGF5S3lBbTVuSi9ROU56RUwyZmtpODVJU3o2WlI3b0xrL045bGV6ODQ5V2thZUpBYUMzZm96c2Zrek9KVXBOQlNWS1RCRkR6K0dyRHV1a0tLL2JDbDBCVHZnT3E5R0k2UWUwUXpFUnV0SVIwUjY3cXptUUxmenRhVVc4UGVOSTcwTVhHZUU9PC9EPjwvUlNBS2V5VmFsdWU+";
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            var persistenceConfig = ConfigurableApplicationContext.Read<SinglePersistenceConfiguration>().From("App_Data\\Persistence.json").AsMachineNameSpecific().ToObject();
            var imageConfig = ConfigurableApplicationContext.Read<ImageConfiguration>().From("App_Data\\Imaging.json").ToObject();
            builder.RegisterModule(new PersistenceModule(persistenceConfig));
            builder.Register(x => new ImageProcessor(imageConfig)).As<IImageProcessor>().SingleInstance();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerRequest();
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterType<MediatorDependencyResolver>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterControllers(assembly);
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.Register(x =>
            {
                var owin = x.Resolve<IOwinContext>();
                return new OwinAuthenticationService(authenticationType, x.Resolve<IUserService>(), owin.Environment);
            }).As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.Register(x => HttpContext.Current.GetOwinContext()).As<IOwinContext>();
            builder.RegisterApiControllers(assembly);
            builder.Register(x => new OAuth2AuthorizationServerSigningKeyHandler(privateKey)).As<IOAuth2AuthorizationServerSigningKeyHandler>().SingleInstance();
            builder.RegisterType<AuthorizationServerHost>().As<IAuthorizationServerHost>().InstancePerRequest();
            builder.RegisterType<AuthorizationService>().As<IOAuth2Service>().InstancePerRequest();
            builder.RegisterType<SymmetricCryptoKeyStore>().As<ICryptoKeyStore>().InstancePerRequest();
            builder.RegisterType<NonceKeyStore>().As<INonceStore>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}