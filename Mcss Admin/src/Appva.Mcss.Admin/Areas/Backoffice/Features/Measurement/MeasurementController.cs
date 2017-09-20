using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Mvc.Security;
using Appva.Mcss.Admin.Application.Common;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Measurements
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("Backoffice"), RoutePrefix("Measurement")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class MeasurementController : Controller
    {
        public MeasurementController()
        {

        }

        #region List

        /// <summary>
        /// The index view
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListMeasurementsModel>))]
        public ActionResult List()
        {
            return this.View();
        }

        #endregion

        #region Create



        #endregion

    }
}