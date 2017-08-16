using Appva.Cqrs;
using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Models
{
    public class ViewTenaMeasurementsModel
    {
        public TenaObservationPeriod ObservationPeriod { get; set; }
        public List<TenaObservationItem> ObservationItems { get; internal set; }
    }
}