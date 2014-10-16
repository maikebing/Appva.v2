// <copyright file="EmailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System.Net.Mail;
    using System.Threading.Tasks;

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
        #region IMessageService Members.

        /// <inheritdoc />
        public void Send(IMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.Send(message as EmailMessage);
            }
        }

        /// <inheritdoc />
        public async Task SendAsync(IMessage message)
        {
            using (var client = new SmtpClient())
            {
                await client.SendMailAsync(message as EmailMessage);
            }
        }

        #endregion
    }
}