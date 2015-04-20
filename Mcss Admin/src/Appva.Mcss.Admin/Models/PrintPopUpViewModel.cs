using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Appva.Mvc.Html.DataAnnotations;
using DataAnnotationsExtensions;

namespace Appva.Mcss.Web.ViewModels {

    public class PrintPopUpViewModel {

        public PrintPopUpViewModel() {
            OnNeedBasis = true;
            StandardSequneces = true;
        }

        public Guid Id { get; set; }

        public Guid ScheduleSettingsId { get; set; }

        [Required]
        [Date]
        [DateLessThan(Target = "EndDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Från datum")]
        public DateTime StartDate { get; set; }

        [Required]
        [Date]
        [DateGreaterThan(Target = "StartDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Till datum")]
        public DateTime EndDate { get; set; }

        [DisplayName("Inkludera vid-behov-läkemedel")]
        public bool OnNeedBasis { get; set; }

        [DisplayName("Inkludera stående ordinationer")]
        public bool StandardSequneces { get; set; }

    }

}