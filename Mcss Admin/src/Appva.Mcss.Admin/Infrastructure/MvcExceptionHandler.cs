// <copyright file="MvcExceptionHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mvc;
    using Appva.Tenant.Identity;
    using Appva.Core.Messaging.RazorMail;

    #endregion

    /// <summary>
    /// Handles Mvc Exceptions, e.g. in controllers etc.
    /// </summary>
    public sealed class MvcExceptionHandler : AbstractExceptionHandler, IWebExceptionHandler
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcExceptionHandler"/> class.
        /// </summary>
        /// <param name="tenants">The <see cref="ITenantService"/></param>
        /// <param name="environment">The <see cref="IApplicationEnvironment"/></param>
        /// <param name="mail">The <see cref="IRazorMailService"/></param>
        public MvcExceptionHandler(ITenantService tenants, IApplicationEnvironment environment, IRazorMailService mail)
            : base(environment, mail)
        {
            this.tenantService = tenants;
        }

        #endregion

        #region AbstractExceptionHandler Members.

        /// <inheritdoc />
        protected override void HandleException(Exception exception)
        {
            ITenantIdentity identity;
            if (this.tenantService.TryIdentifyTenant(out identity))
            {
                var model = ExceptionMail.CreateNew(exception, ExceptionUser.CreateNew(), ExceptionRequest.CreateNew(), identity);
                this.SendMail(identity.Name, model);
            }
        }

        #endregion
    }
}