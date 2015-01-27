// <copyright file="EmailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Appva.Logging;

    #endregion

    /// <summary>
    /// Marker interface for E-mail messaging service.
    /// </summary>
    public interface IEmailService : IMessageService
    {
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
        public void Send(IMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Send(message as EmailMessage);
                }
            } catch (Exception ex)
            {
                Logger.ErrorException("An exception occured in <EmailService>", ex);
            }
        }

        /// <inheritdoc />
        public async Task SendAsync(IMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.SendMailAsync(message as EmailMessage);
                }
            } catch (Exception ex)
            {
                Logger.ErrorException("An exception occured in <EmailService>", ex);
            }
        }

        #endregion
    }
}