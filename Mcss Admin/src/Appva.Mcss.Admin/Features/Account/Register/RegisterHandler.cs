// <copyright file="RegisterHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Application.Security;
    using Application.Security.Identity;
    using Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RegisterHandler : AbstractPasswordHandler<Register>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterHandler"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IFormsAuthentication"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        public RegisterHandler(
            IFormsAuthentication authentication,
            IAccountService accountService,
            IIdentityService identityService)
            : base(authentication, accountService, identityService)
        {
        }

        #endregion
    }
}