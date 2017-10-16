using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class ViewScaleModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Scale.Type ScaleType { get; set; }
        public string Values { get; set; }
    }
}