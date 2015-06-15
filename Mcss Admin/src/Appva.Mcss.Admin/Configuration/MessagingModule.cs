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
    using Autofac;

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
            switch (Configuration.Application.OperationalEnvironment)
            {
                case OperationalEnvironment.Production:
                case OperationalEnvironment.Demo:
                    builder.RegisterType<MailService>().As<ISimpleMailService>();
                    break;
                case OperationalEnvironment.Staging:
                case OperationalEnvironment.Development:
                    builder.RegisterType<NoOpMailService>().As<ISimpleMailService>();
                    break;
            }
        }

        #endregion
    }
}