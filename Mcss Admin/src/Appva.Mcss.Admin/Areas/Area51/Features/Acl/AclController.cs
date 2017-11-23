// <copyright file="AclController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using Appva.Mvc.Security;
    using Appva.Mcss.Admin.Areas.Area51.Features.Acl.AddNewsPermissions;
    using Appva.Mcss.Admin.Areas.Area51.Features.Acl.InstallRoleToRole;
    using Appva.Mcss.Admin.Areas.Area51.Features.Acl.InstallRoleToDelegation;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("acl")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class AclController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AclController"/> class.
        /// </summary>
        /// <param name="authentication">The <see cref="IMediator"/></param>
        public AclController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        #region Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        #endregion

        #region Install Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Install")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Roller och behörigheter är nu installerat!")]
        public ActionResult Install()
        {
            this.mediator.Publish(new InstallAcl());
            return this.RedirectToAction("Index");
        }

        #endregion

        #region Activate Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("activate")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Roller och behörigheter är nu aktiverade!")]
        public ActionResult Activate()
        {
            this.mediator.Send(new ActivateAcl());
            return this.RedirectToAction("Index");
        }

        #endregion

        #region Update Access Control.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("update")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Uppgradering klar!")]
        public ActionResult UpdatePermissions()
        {
            var model = this.mediator.Send(new UpdatePermissionsAcl { UpdateGlobal = false });
            return this.View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("update/global")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Uppgradering för alla kunder klar!")]
        public ActionResult UpdatePermissionsGlobal()
        {
            var model = this.mediator.Send(new UpdatePermissionsAcl { UpdateGlobal = true });
            return this.View("UpdatePermissions", model);
        }

        #endregion

        #region Hotfix #18

        /// <summary>
        /// This hot fix #18 will update fallback roles for user accounts.
        /// </summary>
        /// <returns>Void</returns>
        [Route("hotfix-18")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Hotfix 18 - uppdatering av fallback-roller är genomfört!")]
        public ActionResult Hotfix18()
        {
            this.mediator.Publish(new Hotfix18());
            return this.RedirectToAction("Index");
        }

        #endregion

        #region Add Role-To-Role

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("roletorole")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Rollmappning installerad")]
        public ActionResult InstallRoleToRole(InstallRoleToRole request)
        {
            var model = this.mediator.Send(request);
            return this.View(model);
        }

        #endregion

        #region Add Role-To-Delegation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("roletodelegation")]
        [HttpPost, Validate, ValidateAntiForgeryToken]
        [AlertSuccess("Delegerings-behörigheter installerade!")]
        public ActionResult InstallRoleToDelegation(InstallRoleToDelegation request)
        {
            var model = this.mediator.Send(request);
            return this.View(model);
        }

        #endregion

        #endregion
    }
}