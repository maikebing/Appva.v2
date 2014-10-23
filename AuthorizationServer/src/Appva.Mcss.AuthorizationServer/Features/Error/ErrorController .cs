// <copyright file="ErrorController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ErrorController : Controller
    {
        #region Routes.

        #region NotFound 404.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("404/not-found")]
        public ActionResult NotFound()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("500/internal-server-error")]
        public ActionResult InternalServerError()
        {
            return View();
        }

        #endregion

        #endregion
    }
}