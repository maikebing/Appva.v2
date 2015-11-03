// <copyright file="AbstractPasswordHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// Abstract base change password handler.
    /// </summary>
    /// <typeparam name="TRequest">The password model handling type</typeparam>
    internal abstract class AbstractPasswordHandler<TRequest> : RequestHandler<TRequest, bool>
        where TRequest : AbstractPassword, IRequest<bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IFormsAuthentication"/>.
        /// </summary>
        private readonly IFormsAuthentication authentication;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPasswordHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IFormsAuthentication"/></param>
        /// <param name="accountService">The <see cref="IAccountService"/></param>
        /// <param name="identityService">The <see cref="IIdentityService"/></param>
        protected AbstractPasswordHandler(
            IFormsAuthentication authentication,
            IAccountService accountService,
            IIdentityService identityService)
        {
            this.authentication = authentication;
            this.accountService = accountService;
            this.identityService = identityService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(TRequest message)
        {
            var id = this.identityService.PrincipalId;
            var account = this.accountService.Find(id);
            this.accountService.ChangePassword(account, message.NewPassword);
            this.authentication.SignOut();
            return true;
        }

        #endregion
    }
}