// <copyright file="PersistenceExceptionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using Appva.Core.Environment;
    using Appva.Persistence;
    using Appva.Core.Messaging.RazorMail;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PersistenceExceptionHandler : AbstractExceptionHandler, IPersistenceExceptionHandler
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceExceptionHandler"/> class.
        /// </summary>
        /// <param name="environment">The <see cref="IApplicationEnvironment"/></param>
        /// <param name="mail">The <see cref="IRazorMailService"/></param>
        public PersistenceExceptionHandler(IApplicationEnvironment environment, IRazorMailService mail)
            : base(environment, mail)
        {
        }

        #endregion

        #region AbstractExceptionHandler Members.

        /// <inheritdoc />
        protected override void HandleException(Exception exception)
        {
            var model = ExceptionMail.CreateNew(exception);
            this.SendMail("data source initialization", model);
        }

        #endregion
    }
}