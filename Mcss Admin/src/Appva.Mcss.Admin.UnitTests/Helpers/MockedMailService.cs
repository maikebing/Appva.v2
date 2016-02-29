// <copyright file="MockedMailService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Helpers
{
    #region Imports.

    using Appva.Core.Messaging;
using Appva.Mvc.Messaging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MockedNoOpMailService : AbstractNoOpMailSender<MailMessage>, IRazorMailService, IMailService
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedNoOpMailService"/> class.
        /// </summary>
        private MockedNoOpMailService()
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="NoOpMailService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration</param>
        /// <returns>A new <see cref="MailService"/> instance</returns>
        public static MockedNoOpMailService CreateNew()
        {
            return new MockedNoOpMailService();
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