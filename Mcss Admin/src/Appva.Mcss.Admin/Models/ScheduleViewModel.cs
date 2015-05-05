using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mcss.Admin.Models;
using Appva.Cqrs;

namespace Appva.Mcss.Web.ViewModels {

    public class ScheduleViewModel : IRequest<ListSchedule>
    {

        public ScheduleViewModel() {
            Items = new List<SelectListItem>();
        }

        public virtual Guid Id { get; set; }

        [Required(ErrorMessage = "En lista måste väljas.")]
        [DisplayName("Typ av lista")]
        public virtual Guid ScheduleSetting { get; set; }

        public IEnumerable<SelectListItem> Items { get; set; }

    }

}
