using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class ListScaleModel
    {
        public IList<Scale> Scales { get; set; }
    }
}