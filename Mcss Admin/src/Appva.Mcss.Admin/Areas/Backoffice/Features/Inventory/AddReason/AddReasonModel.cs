using Appva.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public sealed class AddReasonModel : IRequest<bool>
    {
        public string Name { get; set; }
    }
}