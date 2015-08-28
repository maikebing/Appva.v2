// <copyright file="MessagingModule.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using Appva.Core.Messaging;
    using Mvc = Appva.Mvc.Messaging;
    using Autofac;
    using RazorEngine.Configuration;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class MessagingModule : Module
    {
        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="MessagingModule"/> class.
        /// </summary>
        /// <returns>A new <see cref="MessagingModule"/> instance</returns>
        public static MessagingModule CreateNew()
        {
            return new MessagingModule();
        }

        #endregion

        #region Module Overrides.

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            //// Razor template registration.
            builder.Register(x => new TemplateServiceConfiguration
            {
                TemplateManager = new Mvc.CshtmlTemplateManager("Features/Shared/EmailTemplates")
            }).As<ITemplateServiceConfiguration>().SingleInstance();
            switch (Configuration.Application.OperationalEnvironment)
            {
                case OperationalEnvironment.Production:
                case OperationalEnvironment.Demo:
                    builder.RegisterType<MailService>().As<ISimpleMailService>().SingleInstance();
                    builder.RegisterType<Mvc.MailService>().As<Mvc.IRazorMailService>().SingleInstance();
                    break;
                case OperationalEnvironment.Staging:
                case OperationalEnvironment.Development:
                    builder.RegisterType<NoOpMailService>().As<ISimpleMailService>().SingleInstance();
                    builder.RegisterType<Mvc.NoOpMailService>().As<Mvc.IRazorMailService>().SingleInstance();
                    break;
            }
        }

        #endregion
    }
}