// <copyright file="CalendarController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Calendar
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Models;
using Appva.Mvc;
using Appva.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("backoffice"), RoutePrefix("calendar")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class CalendarController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        public CalendarController()
        {
        }

        #endregion

        #region Routes.

        #region List categories.

        /// <summary>
        /// Lists all available categories
        /// </summary>
        /// <returns></returns>
        [Route("category/list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListCategoriesModel>))]
        public ActionResult ListCategories()
        {
            return this.View();
        }

        #endregion

        #region Create category.

        /// <summary>
        /// The create category view
        /// </summary>
        /// <returns></returns>
        [Route("category/create")]
        [HttpGet, Hydrate, Dispatch(typeof(Parameterless<CreateCategoryModel>))]
        public ActionResult CreateCategory()
        {
            return this.View();
        }

        /// <summary>
        /// The create category POST
        /// </summary>
        /// <returns></returns>
        [Route("category/create")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("ListCategories", "Calendar")]
        public ActionResult CreateCategory(CreateCategoryModel request)
        {
            return this.View();
        }

        #endregion

        #region Details category.

        /// <summary>
        /// Category details view
        /// </summary>
        /// <param name="request">The is of the category</param>
        /// <returns>The details view as an ActionResult</returns>
        [Route("category/{id:guid}")]
        [HttpGet, Dispatch()]
        public ActionResult CategoryDetails(Identity<CategoryDetailsModel> request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}