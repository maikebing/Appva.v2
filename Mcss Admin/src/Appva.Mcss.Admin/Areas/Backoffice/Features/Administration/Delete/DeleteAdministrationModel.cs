using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Cqrs;
using Appva.Mcss.Admin.Infrastructure.Models;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class DeleteAdministrationModel : IRequest<Parameterless<ListInventoriesModel>>
    {
        public Guid Id
        {
            get;
            set;
        }
    }
}