using Appva.Cqrs;

namespace Appva.Mcss.Admin.Models
{
    public class ViewTenaMeasurements : Identity<ViewTenaMeasurementsModel>
    {
        public string Period { get; set; }
    }
}