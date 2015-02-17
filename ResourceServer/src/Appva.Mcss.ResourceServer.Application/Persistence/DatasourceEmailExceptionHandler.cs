// <copyright file="DatasourceEmailExceptionHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Core.Messaging;
    using Appva.Logging;
    using Appva.Mcss.ResourceServer.Application.Configuration;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DatasourceEmailExceptionHandler : IDatasourceExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for <see cref="DatasourceEmailExceptionHandler"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<DatasourceEmailExceptionHandler>();

        /// <summary>
        /// The <see cref="IEmailService"/> instance.
        /// </summary>
        private readonly IEmailService service;

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
        /// Initializes a new instance of the <see cref="DatasourceEmailExceptionHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="IEmailService"/></param>
        public DatasourceEmailExceptionHandler(IEmailService service)
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
                Log.ErrorException("An error occured during database initialization", exception);
            }
        }

        /// <inheritdoc />
        public void Handle(IEnumerable<Exception> exceptions)
        {
 	        if (exceptions.IsNotNull())
            {
                var sb = new StringBuilder();
                foreach (var exception in exceptions)
                {
                    Log.ErrorException("An error occured during database initialization", exception);
                    if (this.isProduction)
                    {
                        this.ExceptionAsHtml(sb, exception);
                    }
                }
                if (this.isProduction)
                {
                    try
                    {
                        this.service.Send(new EmailMessage(
                            "noreply@appva.se",
                            "johansalllarsson@appva.se,richard.henriksson@appva.se")
                            {
                                Priority = MailPriority.High,
                                Subject = "Database failed to establish a connection for ResourceServer in " + this.environment,
                                Body = sb.ToString()
                            });
                    }
                    catch (Exception)
                    {
                        Log.Error("Failed to send e-mail");
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