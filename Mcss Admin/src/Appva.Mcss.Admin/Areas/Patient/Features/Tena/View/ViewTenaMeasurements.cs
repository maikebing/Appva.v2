using Appva.Cqrs;
using System;

namespace Appva.Mcss.Admin.Models
{
    public class ViewTenaMeasurements : Identity<ViewTenaMeasurementsModel>
    {
        public Guid PeriodId { get; set; }
    }
}