// <copyright file="CacheController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Caches
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Mcss.Admin.Areas.Area51.Features.Cache.Remove;
    using Appva.Mcss.Admin.Features.Area51.Cache;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("cache")]
    public sealed class CacheController : Controller
    {
        #region Routes.

        #region List Cache.

        /// <summary>
        /// Returns all cache for the current tenant application.
        /// </summary>
        /// <returns>A <see cref="ListCache"/></returns>
        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListCache>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Delete Cache.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Redirects to <see cref="CacheController.List"/></returns>
        [Route("delete")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Cache")]
        public ActionResult Delete(DeleteCache request)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Redirects to <see cref="CacheController.List"/></returns>
        [Route("delete-all")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("List", "Cache")]
        public ActionResult DeleteAll()
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}