// <copyright file="SignatureController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Signature
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("signature")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public class SignatureController : Controller
    {
        #region List

        /// <summary>
        /// Returns a list of signing options.
        /// </summary>
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListSignatureModel>))]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult List(ListSignatureModel request)
        {
            return this.View();
        }

        #endregion

        #region Edit

        /// <summary>
        /// Edit signing options.
        /// </summary>
        [Route("{id:guid}/edit")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Edit(Identity<EditSignatureModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the edit signing option request.
        /// </summary>
        [Route("{id:guid}/edit")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult Edit(EditSignatureModel request, string submitBtn)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Create

        /// <summary>
        /// Creates a new signing option.
        /// </summary>
        [Route("create")]
        [HttpGet, Dispatch] 
        public ActionResult Create(Identity<CreateSignatureModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Returns a list of signing options.
        /// </summary>
        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "signature")]
        public ActionResult Create(CreateSignatureModel request)
        {
            return this.View();
        }

        #endregion

        /// <summary>
        /// Inactivates a signing option.
        /// </summary>
        #region Inactivate
        [Route("{id:guid}/inactivate")]
        [HttpGet, Dispatch("list", "signature")]
        public ActionResult Inactivate(InactivateSignatureModel request)
        {
            return this.View();
        }

        #endregion
    }
}