// <copyright file="ChangePasswordHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Microsoft.AspNet.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ChangePasswordHandler : RequestHandler<ChangePassword, ChangePassword>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordHandler"/> class.
        /// </summary>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        public ChangePasswordHandler(IIdentityService identityService, IAccountService accountService)
        {
            this.identityService = identityService;
            this.accountService = accountService;
        }

        #endregion

        #region RequestHandler<ChangePassword, ChangePassword>.

        /// <inheritdoc />
        public override ChangePassword Handle(ChangePassword message)
        {
            var id = new Guid(this.identityService.Principal.Identity.GetUserId());
            var account = this.accountService.Find(id);
            this.accountService.ChangePassword(account, message.NewPassword);
            throw new NotImplementedException();
        }

        #endregion
    }
}