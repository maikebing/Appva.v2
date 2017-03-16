using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Mcss.Admin.Domain.Models
{
   public sealed class SearchProfileModel
    {
        public bool? IsActive { get; set; }

        public string TaxonFilter
        {
            get;
            set;
        }
    }
}
