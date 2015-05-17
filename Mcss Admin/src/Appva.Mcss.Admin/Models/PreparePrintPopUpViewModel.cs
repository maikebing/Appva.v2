using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mvc;
using DataAnnotationsExtensions;
using Appva.Cqrs;

namespace Appva.Mcss.Web.ViewModels {

    public class PreparePrintPopUpViewModel : IRequest<bool> {

        public Guid Id { get; set; }

        public Guid ScheduleId { get; set; }

        [Required]
        [Date]
        [DateLessThan(Target = "PrintEndDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Från datum")]
        public DateTime PrintStartDate { get; set; }

        [Required]
        [Date]
        [DateGreaterThan(Target = "PrintStartDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Till datum")]
        public DateTime PrintEndDate { get; set; }
    }
}