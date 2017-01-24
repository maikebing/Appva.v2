// <copyright file="OrganizationController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Organization
{
    #region Imports

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("organization")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class OrganizationController : Controller
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationController"/> class.
        /// </summary>
        public OrganizationController()
        {
        }

        #endregion

        #region Routes

        #region List

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListOrganizationModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create

        /// <summary>
        /// Create a new organizationalunit
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/create")]
        [HttpGet, Dispatch]
        public ActionResult Create(Identity<CreateOrganizationalUnitModel> request)
        {
            return this.View();
        }


        /// <summary>
        /// Create a new organizationalunit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "organization")]
        public ActionResult Create(CreateOrganizationalUnitModel request)
        {
            return this.View();
        }


        #endregion

        #region Update

        /// <summary>
        /// Update an organizationalunit
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpGet, Dispatch]
        public ActionResult Update(Identity<UpdateOrganizationalUnitModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Update an organizationalunit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/update")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "organization")]
        public ActionResult Update(UpdateOrganizationalUnitModel request)
        {
            return this.View();
        }

        #endregion

        #region delete 

        /// <summary>
        /// Delete an organizationalunit
        /// </summary>
        /// <returns></returns>
        [Route("{id:guid}/delete")]
        [HttpGet, Dispatch("List", "Organization")]
        public ActionResult Delete(DeleteOrganizationalUnitModel request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}