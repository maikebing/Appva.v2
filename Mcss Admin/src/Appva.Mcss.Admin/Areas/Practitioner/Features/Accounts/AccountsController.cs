// <copyright file="AccountsController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RouteArea("Practitioner"), RoutePrefix("Accounts")]
    public class AccountsController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/> dispatcher.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region List.

        /// <summary>
        /// Returns a list of <c>Account</c> by filters.
        /// </summary>
        /// <param name="request">The query and filtering</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Dispatch,/*Permissions("ad0b7efb-589b-4762-acd6-a46d009a21ad"),*/ Route("List")]
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
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Accounts")]
        public ActionResult Update(UpdateAccount request)
        {
            return this.View();
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
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Accounts")]
        public ActionResult UpdateRoles(UpdateRolesForm request)
        {
            return this.View();
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
        [HttpGet, Dispatch("List", "Accounts")]
        public ActionResult Pause(PauseAccount request)
        {
            return this.View();
        }

        #endregion

        #region Resume.

        /// <summary>
        /// Unpauses an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [Route("{id:guid}/unpause")]
        [HttpGet, Dispatch("List", "Accounts")]
        public ActionResult Unpause(UnPauseAccount request)
        {
            return this.View();
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

        

        /*
        #region Change Password.

        /// <summary>
        /// Returns the change password view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
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
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var account = Identity();
            if (!account.HashedPassword.Equals(EncryptionUtils.Hash(model.CurrentPassword, account.Salt)))
            {
                ModelState.AddModelError("CurrentPassword", "Nuvarande lösenord stämmer ej.");
            }
            else
            {
                AccountService.ChangePassword(account, model.NewPassword);
                TempData["Message"] = "Ditt lösenord har ändrats.";
                return this.RedirectToAction("Index", "Home");
            }
            return View(new ChangePasswordViewModel());
        }

        #endregion
        
        */
        #endregion
    }
}