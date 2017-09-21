using Appva.Cqrs;
using Appva.Mcss.Admin.Domain.Entities;
using System;

namespace Appva.Mcss.Admin.Models
{
    public class UpdateScheduleForm : IRequest<ListSchedule>
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }
        public string Name { get; set; }
        public bool IsCollectingGivenDosage { get; set; }
    }
}