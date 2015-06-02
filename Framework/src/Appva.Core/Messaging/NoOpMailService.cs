// <copyright file="NoOpMailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Messaging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using Appva.Core.Logging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoOpMailService : AbstractNoOpMailSender<MailMessage>, ISimpleMailService, IMailService
    {
        #region Variables.

        /// <summary>
        /// Logging for <see cref="EmailMessage"/>.
        /// </summary>
        private static readonly ILog Logger = LogProvider.For<NoOpMailService>();

        #endregion

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
            Logger.DebugJson(message);
            return message;
        }

        #endregion
    }
}