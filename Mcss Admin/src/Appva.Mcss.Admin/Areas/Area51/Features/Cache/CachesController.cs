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
    using Appva.Mvc.Filters;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("cache")]
    public sealed class CacheController : Controller
    {
        #region Routes.

        /// <summary>
        /// Returns all cache for the current tenant application.
        /// </summary>
        /// <returns>The cache view</returns>
        [HttpGet, Route("list"), Dispatch(typeof(Parameterless<ListCache>))]
        public ActionResult List()
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Route("remove"), Dispatch("List", "Cache")]
        public ActionResult Remove(RemoveCacheKey request)
        {
            return this.View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost, Validate, ValidateAntiForgeryToken, Route("removeall"), Dispatch("List", "Cache")]
        public ActionResult RemoveAll()
        {
            return this.View();
        }

        #endregion
    }
}