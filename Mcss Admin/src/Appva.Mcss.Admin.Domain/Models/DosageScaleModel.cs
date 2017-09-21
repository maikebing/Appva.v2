using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Mcss.Admin.Domain.Models
{
    public class DosageScaleModel
    {
        public Guid Id { get; internal set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public List<double> Values { get; set; }
    }
}
