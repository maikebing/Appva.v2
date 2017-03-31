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

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mvc.Security;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mcss.Admin.Models;
    using Create;

    #endregion

    [RouteArea("backoffice"), RoutePrefix("signature")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public class SignatureController : Controller
    {
        #region List

        /// <summary>
        /// List all signing options
        /// </summary>
        /// <returns></returns>
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
        /// Edit signing options
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpGet, Hydrate, Dispatch]
        public ActionResult Edit(Identity<EditSignatureModel> request)
        {
            return this.View();
        }

        /// <summary>
        /// Handles the edit signing request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("{id:guid}/edit")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch]
        public ActionResult Edit(EditSignatureModel request, string submitBtn)
        {
            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        #endregion

        #region Create

        [Route("create")]
        [HttpGet, Dispatch] 
        public ActionResult Create(Identity<CreateSignatureModel> request)
        {
            return this.View();
        }

        [Route("create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("list", "signature")]
        public ActionResult Create(CreateSignatureModel request)
        {
            return this.View();
        }

        #endregion

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