using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Scale
{
    [RouteArea("backoffice"), RoutePrefix("scale")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class ScaleController : Controller
    {
        #region Variables.

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        public ScaleController()
        {
        }

        #endregion

        #region Routes

        #region List

        [Route("list")]
        [HttpGet, Dispatch(typeof(Parameterless<ListScaleModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #endregion
    }
}