﻿// <copyright file="MailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using Appva.Core.Messaging;
    using RazorEngine.Configuration;
    using RazorEngine.Templating;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IRazorMailService : IMailSender<MailMessage>
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MailService : AbstractMailSender<MailMessage>, IRazorMailService, IMailService
    {
        #region Variables.

        /// <summary>
        /// The razor engine service.
        /// </summary>
        private readonly IRazorEngineService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        public MailService(ITemplateServiceConfiguration configuration)
        {
            this.service = RazorEngineService.Create(configuration);
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="MailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <returns>A new <see cref="MailService"/> instance</returns>
        public static MailService CreateNew(ITemplateServiceConfiguration configuration)
        {
            return new MailService(configuration);
        }

        #endregion

        #region AbstractMailSender<MailMessage> Overrides.

        /// <inheritdoc />
        protected override MailMessage Handle(MailMessage message)
        {
            message.Body = this.service.RunCompile(message.TemplateName, message.TemplateModelType, message.TemplateModel);
            message.IsBodyHtml = true;
            return message;
        }

        #endregion
    }
}