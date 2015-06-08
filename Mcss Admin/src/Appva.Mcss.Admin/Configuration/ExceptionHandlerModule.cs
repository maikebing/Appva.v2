// <copyright file="ExceptionHandlerModule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Core.Exceptions;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using Appva.Mvc.Messaging;
    using Appva.Persistence;
    using Autofac;
    using Autofac.Integration.Mvc;
    using RazorEngine.Configuration;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ExceptionHandlerModule : Module
    {
        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ExceptionHandlerModule"/> class.
        /// </summary>
        /// <returns>A new <see cref="ExceptionHandlerModule"/> instance</returns>
        public static ExceptionHandlerModule CreateNew()
        {
            return new ExceptionHandlerModule();
        }

        #endregion

        #region Module Overrides.

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            //// Razor template registration.
            builder.Register(x => new TemplateServiceConfiguration
            {
                TemplateManager = new CshtmlTemplateManager("Features/Shared/EmailTemplates")
            }).As<ITemplateServiceConfiguration>().SingleInstance();
            
            //// Mvc Exception Global Filter registration.
            builder.RegisterType<ExceptionFilter>().AsExceptionFilterFor<Controller>().InstancePerRequest();

            //// Admin Mvc Exception Handler registration.
            builder.RegisterType<MvcExceptionHandler>().As<IWebExceptionHandler>().SingleInstance();

            //// Admin Persistence Exception Handler registration
            builder.RegisterType<PersistenceExceptionHandler>().As<IPersistenceExceptionHandler>().SingleInstance();

            //// Switch Mail service by environment.
            switch (Configuration.Application.OperationalEnvironment)
            {
                case OperationalEnvironment.Production:
                case OperationalEnvironment.Demo:
                    builder.RegisterType<MailService>().As<IRazorMailService>().SingleInstance();
                    break;
                case OperationalEnvironment.Staging:
                case OperationalEnvironment.Development:
                    builder.RegisterType<NoOpMailService>().As<IRazorMailService>().SingleInstance();
                    break;
            }
        }

        #endregion
    }
}