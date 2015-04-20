using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mvc.Html.DataAnnotations;
using DataAnnotationsExtensions;

namespace Appva.Mcss.Web.ViewModels {

    public class PreparePrintPopUpViewModel {

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