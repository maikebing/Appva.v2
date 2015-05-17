// <copyright file="EmailMessage.cs" company="Appva AB">
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
    /// Represents an E-mail message.
    /// </summary>
    public sealed class EmailMessage : MailMessage
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        public EmailMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        /// <param name="from">The origin E-mail address</param>
        /// <param name="to">The receipient E-mail address</param>
        /// <param name="isBodyHtml">Whether or not the E-mail body is HTML</param>
        public EmailMessage(string from, string to, bool isBodyHtml = true)
            : base(from, to)
        {
            this.IsBodyHtml = isBodyHtml;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailMessage"/> class.
        /// </summary>
        /// <param name="from">The origin E-mail address</param>
        /// <param name="to">The receipient E-mail address</param>
        /// <param name="subject">The E-mail subject</param>
        /// <param name="body">The E-mail body</param>
        /// <param name="isBodyHtml">Whether or not the E-mail body is HTML</param>
        public EmailMessage(string from, string to, string subject, string body, bool isBodyHtml = true)
            : base(from, to, subject, body)
        {
            this.IsBodyHtml = isBodyHtml;
        }

        #endregion
    }
}
