// <copyright file="CacheController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Audit
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Areas.Area51.Features.Cache.Remove;
    using Appva.Mcss.Admin.Features.Area51.Cache;
    using Appva.Mcss.Admin.Infrastructure.Attributes;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("audit")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class AuditController : Controller
    {
        #region Routes.

        #region Index.

        [Route()]
        [HttpGet, Dispatch(typeof(Parameterless<IsAuditPermissionsInstalled>))]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Install.

        [Route("install")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Index", "Audit")]
        public ActionResult Install(InstallAuditPermissions request)
        {
            return this.View();
        }

        #endregion

        #region Install Configuration.

        [Route("configuration/install")]
        [HttpGet, Dispatch(typeof(Parameterless<AuditConfiguration>))]
        public ActionResult Configuration()
        {
            return this.View();
        }

        [Route("configuration/install")]
        [HttpPost, Validate, ValidateAntiForgeryToken, Dispatch("Configuration", "Audit")]
        public ActionResult Configuration(AuditConfiguration request)
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}