// <copyright file="AccountController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Account
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Areas.Area51.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("account")]
    [Permissions(Permissions.Area51.ReadValue)] 
    public sealed class AccountController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
        }

        #endregion

        #region Routes.

        #region Duplicated accounts

        /// <summary>
        /// List all accounts whit duplicated uniqueidentifiers
        /// </summary>
        /// <returns></returns>
        [Route("duplicates/list")]
        [HttpGet, Dispatch(typeof(Parameterless<List<DuplicatedAccount>>))]
        public ActionResult ListDuplicates()
        {
            return this.View();
        }

        [Route("duplicates/{id:guid}/delete")]
        public ActionResult DeleteDuplicate(DeleteDuplicate request)
        { 
            return this.View();
        }

        #endregion

        #endregion
    }
}