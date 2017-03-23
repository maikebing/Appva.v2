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

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.GeneralSettings
{

    [RouteArea("backoffice"), RoutePrefix("generalsettings")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public sealed class GeneralSettingsController : Controller
    {
        [Route("list")]
        [Dispatch(typeof(Parameterless<ListGeneralSettingsModel>))]
        public ActionResult List()
        {

            return this.View();
        }




    }
}