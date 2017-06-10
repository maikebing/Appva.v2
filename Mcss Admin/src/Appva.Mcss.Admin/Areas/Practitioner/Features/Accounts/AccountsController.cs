// <copyright file="AccountsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts
{
    #region Imports.

    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
using Appva.Mcss.Admin.Areas.Practitioner.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("Practitioner"), RoutePrefix("Accounts")]
    public class AccountsController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

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
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        public AccountsController(IMediator mediator, IAccountService accountService,
            IIdentityService identityService)
        {
            this.mediator = mediator;
            this.accountService = accountService;
            this.identityService = identityService;
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of <c>Account</c> by filters.
        /// </summary>
        /// <param name="request">The query and filtering</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("List")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.ReadValue)]
        public ActionResult List(ListAccount request)
        {
            return this.View();
        }

        #endregion

        #region Create.

        /// <summary>
        /// Return the create account view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateAccountModel>))]
        [PermissionsAttribute(Permissions.Practitioner.CreateValue)]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Creates an account if the model is valid.
        /// </summary>
        /// <param name="model">The account model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("Create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Accounts")]
        [PermissionsAttribute(Permissions.Practitioner.CreateValue)]
        public ActionResult Create(CreateAccountModel request)
        {
            return this.View();
        }

        #endregion

        #region Update.

        /// <summary>
        /// The account edit view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.UpdateValue)]
        public ActionResult Update(Identity<UpdateAccount> request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the account if valid.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="model">The account model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.UpdateValue)]
        public ActionResult Update(UpdateAccount request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Update Roles.

        /// <summary>
        /// Returns the view to update roles.
        /// </summary>
        /// <param name="request">The update roles request</param>
        /// <returns>An <see cref="UpdateRolesForm"/></returns>
        [Route("{id:guid}/roles/update")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.UpdateRolesValue)]
        public ActionResult UpdateRoles(UpdateRoles request)
        {
            return this.View();
        }

        /// <summary>
        /// Updates the roles for the selected account.
        /// </summary>
        /// <param name="request">The form model</param>
        /// <returns>
        /// Redirects to <see cref="AccountsController.List"/>
        /// </returns>
        [Route("{id:guid}/roles/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.UpdateRolesValue)]
        public ActionResult UpdateRoles(UpdateRolesForm request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Inactivate.

        /// <summary>
        /// Inactivates an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/inactivate")]
        [HttpGet, Dispatch("List", "Accounts")]
        [PermissionsAttribute(Permissions.Practitioner.InactivateValue)]
        public ActionResult Inactivate(InactivateAccount request)
        {
            return this.View();
        }

        #endregion

        #region Reactivate.

        /// <summary>
        /// Activates an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/reactivate")]
        [HttpGet, Dispatch("List", "Accounts")]
        [PermissionsAttribute(Permissions.Practitioner.ReactivateValue)]
        public ActionResult Reactivate(ReactivateAccount request)
        {
            return this.View();
        }

        #endregion

        #region Pause.

        /// <summary>
        /// Pauses an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/pause")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.PauseValue)]
        public ActionResult Pause(PauseAccount request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Resume.

        /// <summary>
        /// Unpauses an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/unpause")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.ResumeValue)]
        public ActionResult Unpause(UnPauseAccount request)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region IsUnique Json.

        /// <summary>
        /// Checks whether or not the personal identification number is unique
        /// across the system.
        /// </summary>
        /// <param name="id">Optional account id</param>
        /// <param name="uniqueIdentifier">The national/personal identification number</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("is-unique")]
        [HttpPost, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult VerifyUniqueAccount(VerifyUniqueAccount request)
        {
            return this.JsonPost();
        }

        #endregion

        #region QuickSearch Json.

        /// <summary>
        /// Returns a list of accounts by search results.
        /// </summary>
        /// <param name="request">The <see cref="QuickSearchAccount"/>.</param>
        /// <returns><see cref="JsonResult"/></returns>
        [Route("quicksearch")]
        [HttpGet, Dispatch, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public DispatchJsonResult QuickSearch(QuickSearchAccount request)
        {
            return this.JsonGet();
        }

        #endregion

        #region Change Password.

        /// <summary>
        /// Returns the change password view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("change-password")]
        [HttpGet, Hydrate]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Updates the password if valid.
        /// </summary>
        /// <param name="model">The password model</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("change-password")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            var account = this.accountService.Find(this.identityService.PrincipalId);
            if (! account.AdminPassword.Equals(EncryptionUtils.Hash(model.CurrentPassword, account.Salt)))
            {
                ModelState.AddModelError("CurrentPassword", "Nuvarande lösenord stämmer ej.");
            }
            else
            {
                this.accountService.ChangePassword(account, model.NewPassword);
                TempData["Message"] = "Ditt lösenord har ändrats.";
                return this.RedirectToAction("ChangePassword", "Accounts");
            }
            return View(model);
        }

        #endregion

        #region Show user credentials

        /// <summary>
        /// Shows the credentials for a user
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/credentials")]
        [HttpGet, Hydrate, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.ReadCredentialsValue)]
        public ActionResult ShowCredentials(Identity<UserCredentialsModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Creates a document with all users credentials
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("credentials")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Practitioner.ReadCredentialsValue)]
        public ActionResult GetCredentials(UserCredentialsFile request)
        {
            return this.ExcelFile();
        }

        #endregion

        #endregion
    }
}