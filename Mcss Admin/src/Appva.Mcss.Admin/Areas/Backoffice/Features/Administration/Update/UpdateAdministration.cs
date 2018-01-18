using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Cqrs;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class UpdateAdministration : IRequest<UpdateAdministrationModel>
    {
        public Guid Id
        {
            get;
            set;
        }
    }
}