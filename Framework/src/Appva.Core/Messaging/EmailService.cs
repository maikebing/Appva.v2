// <copyright file="EmailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Appva.Core.Resources;
    using Logging;
    using Validation;

    #endregion

    /// <summary>
    /// Marker interface for E-mail messaging service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an E-mail message.
        /// </summary>
        /// <param name="message">The E-mail message</param>
        void Send(EmailMessage message);

        /// <summary>
        /// Sends an E-mail message asyncronous.
        /// </summary>
        /// <param name="message">The E-mail message</param>
        /// <returns>The <see cref="Task"/></returns>
        Task SendAsync(EmailMessage message);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EmailService : IEmailService
    {
        #region Variables.

        /// <summary>
        /// Logging for <see cref="EmailMessage"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<EmailMessage>();

        #endregion

        #region IMessageService Members.

        /// <inheritdoc />
        public void Send(EmailMessage message)
        {
            Requires.NotNull(message, "message");
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Send(message);
                }
            } 
            catch (Exception ex)
            {
                Logger.Error(ex, ExceptionWhen.SendingEmail, message.To.ToString(), message.Subject, message.Body);
            }
        }

        /// <inheritdoc />
        public async Task SendAsync(EmailMessage message)
        {
            Requires.NotNull(message, "message");
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.SendMailAsync(message).ConfigureAwait(false);
                }
            } 
            catch (Exception ex)
            {
                Logger.Error(ex, ExceptionWhen.SendingEmail, message.To.ToString(), message.Subject, message.Body);
            }
        }

        #endregion
    }
}
