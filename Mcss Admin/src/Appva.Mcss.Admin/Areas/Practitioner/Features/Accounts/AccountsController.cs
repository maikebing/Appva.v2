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
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Features.Accounts.Create;
    using Appva.Mcss.Admin.Features.Accounts.List;
    using Appva.Mvc.Filters;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RouteArea("Practitioner", AreaPrefix="admin"), RoutePrefix("account")]
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

        #region List View.

        /// <summary>
        /// Returns a list of <c>Account</c> by filters.
        /// </summary>
        /// <param name="q">The query</param>
        /// <param name="page">The current page</param>
        /// <param name="filterByDelegation">A <c>Delegation</c> id to filter by</param>
        /// <param name="filterByTitle">Optional <c>Taxon</c> id to filter <c>Role</c> by</param>
        /// <param name="filterByCreatedBy">Optional filtering by accounts created by the current user</param>
        /// <param name="isActive">Optional filtering by <c>Account.IsActive</c></param>
        /// <param name="isPaused">Optional filtering by <c>Account.IsPaused</c></param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Permissions("ad0b7efb-589b-4762-acd6-a46d009a21ad"), Route("List")]
        public ActionResult List(string q, int? page, Guid? filterByDelegation, Guid? filterByTitle, bool filterByCreatedBy = false, bool isActive = true, bool isPaused = false)
        {
            return this.View(this.mediator.Send<ListAccountModel>(new ListAccountCommand
                {
                    SearchQuery = q,
                    CurrentPageNumber = page,
                    DelegationFilterId = filterByDelegation,
                    RoleFilterId = filterByTitle,
                    IsFilterByCreatedByEnabled = filterByCreatedBy,
                    IsFilterByIsActiveEnabled = isActive,
                    IsFilterByIsPausedEnabled = isPaused
                }));
            /*var user = Identity();
            var viewModel = new AccountListViewModel
            {
                FilterByCreatedBy = filterByCreatedBy,
                FilterByDelegation = filterByDelegation,
                FilterByTitle = filterByTitle,
                Delegations = TaxonomyService.FindChildNodes(HierarchyUtils.Delegation).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Titles = RoleService.Match(RoleUtils.TitlePrefix).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Search = ExecuteCommand<SearchViewModel<AccountViewModel>>(new SearchAccountCommand
                {
                    Identity = user,
                    SearchQuery = q,
                    Page = page,
                    FilterByDelegation = filterByDelegation,
                    FilterByTitle = filterByTitle,
                    FilterByCreatedByIdentity = filterByCreatedBy,
                    IsActive = isActive,
                    IsPaused = isPaused
                }),
                IsActive = isActive,
                IsPaused = isPaused
            };
            this.LogService.Info("Användare {0} läste medarbetarlista sida {1}".FormatWith(user.FullName, viewModel.Search.PageNumber), user, null, LogType.Read);
            return View(viewModel);*/
        }

        #endregion

        #region Create View.

        /// <summary>
        /// Return the create account view.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Hydrate]
        public ActionResult Create()
        {
            return this.View(this.mediator.Send<CreateAccountModel>(new CreateAccountModel()));
            /*var GenerateClientPassword = SettingService.GetAccountSettings().ContainsKey("GenerateClientPassword") ? SettingService.GetAccountSettings()["GenerateClientPassword"] : false;
            return View(new AccountFormViewModel
            {
                Taxons = TaxonomyHelper.SelectList(TaxonomyService.Find(HierarchyUtils.Organization)),
                Titles = TitleHelper.SelectList(RoleService.Match(RoleUtils.TitlePrefix)),
                PasswordFieldIsVisible = (bool)GenerateClientPassword == false
            });*/
        }

        /// <summary>
        /// Creates an account if the model is valid.
        /// </summary>
        /// <param name="model">The account model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Create(CreateAccountModel model, FormCollection collection)
        {
            return this.View(this.mediator.Send<CreateAccountModel>(new CreateAccountModel()));
            /*if (ModelState.IsValid)
            {
                Taxon taxon = null;
                if (model.Taxon.IsEmpty())
                {
                    taxon = HierarchyUtils.FindRootNode(TaxonomyService.FindRootNodes(HierarchyUtils.Organization));
                }
                else
                {
                    taxon = this.TaxonomyService.Get(HierarchyUtils.ParseToGuid(collection));
                }
                var role = this.RoleService.Get(RoleUtils.ParseToGuid(model.TitleRole));
                if (taxon.IsNotNull() && role.IsNotNull())
                {
                    var roles = new List<Role> { 
                        RoleService.Get(RoleUtils.DeviceAccount),
                        role
                    };
                    var password = (bool)SettingService.GetAccountSettings().GetOrDefault("GenerateClientPassword", false) ? AccountUtils.GenerateClientPassword() : model.Password;
                    if (role.MachineName.StartsWith(RoleUtils.TitleNurse))
                    {
                        AccountService.CreateBackendAccount(model.FirstName, model.LastName, model.UniqueIdentifier, model.Email, "abc123ABC", taxon, roles, password);
                    }
                    else
                    {
                        AccountService.Create(model.FirstName, model.LastName, model.UniqueIdentifier, model.Email, password, taxon, roles);
                    }
                    return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
                }
            }
            TaxonomyHelper.SelectList(TaxonomyService.Find(HierarchyUtils.Organization));
            return View(model);*/
        }

        #endregion

        /*
        
        
        

        #region Edit View.

        /// <summary>
        /// The account edit view.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet, Hydrate]
        public ActionResult Edit(Guid id)
        {
            var account = Session.Get<Account>(id);
            var isAdminStaff = account.Roles.Any(x => x.MachineName.StartsWith(RoleUtils.AdminPersonnel));
            return View(new AccountFormViewModel
            {
                AccountId = id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                UniqueIdentifier = account.UniqueIdentifier,
                Email = account.Email,
                Password = account.Password,
                Username = account.UserName,
                TitleRole = TitleHelper.GetTitleID(account.Roles, RoleService.Get(RoleUtils.TitleAssistentNurse)),
                Titles = isAdminStaff ? TitleHelper.SelectList(RoleService.Match(RoleUtils.AdminPersonnel)) : TitleHelper.SelectList(RoleService.Match(RoleUtils.TitlePrefix)),
                IsAdminStaff = isAdminStaff,
                Taxon = account.Taxon.Id.ToString(),
                Taxons = TaxonomyHelper.SelectList(account.Taxon, TaxonomyService.Find(HierarchyUtils.Organization)),
                EditableClientPassword = (bool)SettingService.GetAccountSettings().ContainsKey("EditableClientPassword") ? (bool)SettingService.GetAccountSettings()["EditableClientPassword"] : true,
                DisplayUsername = SettingService.DisplayAccountUsername()
            });
        }

        /// <summary>
        /// Updates the account if valid.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <param name="model">The account model</param>
        /// <param name="collection">The form collection</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, AccountFormViewModel model, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Taxon taxon = null;
                if (model.Taxon.IsEmpty())
                {
                    taxon = HierarchyUtils.FindRootNode(TaxonomyService.FindRootNodes(HierarchyUtils.Organization));
                }
                else
                {
                    taxon = TaxonomyService.Get(HierarchyUtils.ParseToGuid(collection));
                }
                var account = AccountService.Get(id);
                var role = RoleService.Get(RoleUtils.ParseToGuid(model.TitleRole));
                if (account.IsNotNull() && taxon.IsNotNull() && role.IsNotNull())
                {
                    var roles = new List<Role>() { role };
                    AccountService.Update(account, model.FirstName, model.LastName, model.UniqueIdentifier, model.Email, model.Password, taxon, roles);
                    return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
                }
            }
            model.Taxons = TaxonomyHelper.SelectList(TaxonomyService.Find(HierarchyUtils.Organization));
            return View(model);
        }

        #endregion

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

        #region Inactivate And Activate View.

        /// <summary>
        /// Inactivates an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Inactivate(Guid id)
        {
            AccountService.Inactivate(id);
            return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
        }

        /// <summary>
        /// Activates an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Activate(Guid id)
        {
            AccountService.Activate(id);
            return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
        }

        #endregion

        #region Pause And Unpause View.

        /// <summary>
        /// Pauses an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult Pause(Guid id)
        {
            AccountService.Pause(id);
            return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
        }

        /// <summary>
        /// Unpauses an account.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns><see cref="ActionResult"/></returns>
        [HttpGet]
        public ActionResult UnPause(Guid id)
        {
            AccountService.UnPause(id);
            return this.RedirectToAction(c => c.List(null, null, null, null, false, true, false));
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
        [HttpPost, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsUnique(Guid? id, string uniqueIdentifier)
        {
            return Json(ExecuteCommand<bool>(new IsUniqueIdentifierCommand<Account>
            {
                Id = id,
                UniqueIdentifier = uniqueIdentifier
            }), JsonRequestBehavior.DenyGet);
        }

        #endregion

        #region QuickSearch Json.

        /// <summary>
        /// Returns a list of accounts by search results.
        /// </summary>
        /// <param name="term">The term to search for</param>
        /// <param name="isActive">Filter accounts on is active accounts</param>
        /// <param name="isPaused">Filter accounts on is paused accounts</param>
        /// <returns><see cref="JsonResult"/></returns>
        [HttpGet, OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult QuickSearch(string term, bool isActive = true, bool isPaused = false)
        {
            return Json(ExecuteCommand<IEnumerable<object>>(new AccountQuickSearch
            {
                Term = term,
                IsActive = isActive,
                IsPaused = isPaused,
                Identity = Identity()
            }), JsonRequestBehavior.AllowGet);
        }

        #endregion
        */
        #endregion
    }
}