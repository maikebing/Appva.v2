using Appva.Cqrs;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class TestMeasurement : IRequest<TestMeasurementModel>
    {
        public Guid Id { get; set; }
        public Guid MeasurementId { get; set; }
    }
}