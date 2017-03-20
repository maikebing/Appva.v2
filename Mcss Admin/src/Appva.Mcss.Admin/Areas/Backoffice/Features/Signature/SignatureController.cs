using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers;

namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Signature
{

    [RouteArea("backoffice"), RoutePrefix("signature")]
    [Permissions(Permissions.Backoffice.ReadValue)]
    public class SignatureController : Controller
    {
        // GET: Backoffice/Signature

        [Route("list")]
        [HttpGet, Dispatch]
        [PermissionsAttribute(Permissions.Backoffice.ReadValue)]
        public ActionResult List(ListSignatureModel request)
        {
            return this.View();
        }
    }
}