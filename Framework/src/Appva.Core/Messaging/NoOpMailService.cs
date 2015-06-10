// <copyright file="NoOpMailService.cs" company="Appva AB">
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
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoOpMailService : AbstractNoOpMailSender<MailMessage>, ISimpleMailService, IMailService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpMailService"/> class.
        /// </summary>
        public NoOpMailService()
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="NoOpMailService"/> class.
        /// </summary>
        /// <returns>A new <see cref="MailService"/> instance</returns>
        public static NoOpMailService CreateNew()
        {
            return new NoOpMailService();
        }

        #endregion

        #region AbstractMailSender<MailMessage> Overrides.

        /// <inheritdoc />
        protected override MailMessage Handle(MailMessage message)
        {
            return message;
        }

        #endregion
    }
}