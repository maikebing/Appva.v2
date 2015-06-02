// <copyright file="SignInHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Tenant.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SignInHandler : RequestHandler<SignIn, SignInForm>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="ITenantService"/></param>
        public SignInHandler(ITenantService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override SignInForm Handle(SignIn message)
        {
            ITenantIdentity identity = null;
            this.service.TryIdentifyTenant(out identity);
            return new SignInForm
            {
                Tenant = identity.Name,
                ReturnUrl = message.ReturnUrl
            };
        }

        #endregion
    }
}