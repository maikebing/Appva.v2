// <copyright file="TenantPersistenceExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Text;
    using Appva.Core.Configuration;
    using Appva.Core.Exceptions;
    using Appva.Core.Extensions;
    using Appva.Core.Messaging;
    using Appva.Core.Logging;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenantPersistenceExceptionHandler : IPersistenceExceptionHandler, IExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="TenantPersistenceExceptionHandler"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<TenantPersistenceExceptionHandler>();

        /// <summary>
        /// The <see cref="IEmailService"/> instance.
        /// </summary>
        private readonly ISimpleMailService service;

        /// <summary>
        /// Whether or not the system runs in production mode.
        /// </summary>
        private readonly bool isProduction;

        /// <summary>
        /// The environment mode.
        /// </summary>
        private readonly string environment;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantPersistenceExceptionHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="IEmailService"/></param>
        public TenantPersistenceExceptionHandler(ISimpleMailService service)
        {
            this.service = service;
            this.isProduction = ConfigurableApplicationContext.Get<ResourceServerConfiguration>().IsProduction;
            this.environment = this.isProduction ? "Production" : "Development/Test";
        }

        #endregion
    
        #region IDatasourceExceptionHandler Members.

        /// <inheritdoc />
        public void Handle(Exception exception)
        {
            if (exception.IsNotNull())
            {
                Log.Error(exception);
            }
        }

        /// <inheritdoc />
        public void Handle(AggregateException exception)
        {
            if (exception.IsNotNull())
            {
                var sb = new StringBuilder();
                foreach (var ex in exception.InnerExceptions)
                {
                    Log.Error(exception);
                    if (this.isProduction)
                    {
                        this.ExceptionAsHtml(sb, ex);
                    }
                }
                if (this.isProduction)
                {
                    try
                    {
                        var mail = new MailMessage();
                        mail.To.Add("johansalllarsson@appva.se,richard.henriksson@appva.se");
                        mail.Priority = MailPriority.High;
                        mail.Body = sb.ToString();
                        mail.Subject = "Database failed to establish a connection for ResourceServer in " + this.environment;
                        this.service.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex);
                    }
                }
            }
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Converts an exception to an html message.
        /// </summary>
        /// <param name="sb">The string builder</param>
        /// <param name="exception">The exception</param>
        private void ExceptionAsHtml(StringBuilder sb, Exception exception)
        {
            try
            {
                sb.Append("<h2>{0}</h2><p>{1}</p><p><pre>{2}</pre></p>".FormatWith(
                        exception.GetType().Name,
                        exception.Message,
                        exception.StackTrace));
                sb.Append(("<h3>System information</h3>" +
                        "<p><strong>Environment:</strong> {0}</p>" +
                        "<p><strong>Site:</strong> {1}</p>" +
                        "<p><strong>Version:</strong> {2}</p>" +
                        "<p><strong>Machine:</strong> {3}</p>").FormatWith(
                        this.environment,
                        "ResourceServer",
                        "1.0.0",
                        Environment.MachineName));
            } 
            catch (Exception)
            {
                //// No ops.
            }
        }

        #endregion
    }
}