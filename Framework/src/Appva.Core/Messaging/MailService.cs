// <copyright file="MailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System.Net.Mail;

    #endregion

    /// <summary>
    /// Marker interface for E-mail messaging service.
    /// </summary>
    public interface IMailService
    {
    }

    /// <summary>
    /// Allows applications to send e-mail by using the Simple Mail Transfer Protocol 
    /// (SMTP).
    /// </summary>
    public sealed class MailService : AbstractMailSender<MailMessage>, IMailService
    {
        #region AbstractMailSender<MailMessage> Overrides.

        /// <inheritdoc />
        protected override MailMessage Handle(MailMessage message)
        {
            return message;
        }

        #endregion
    }
}
