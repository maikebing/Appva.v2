using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mvc.Html.DataAnnotations;
using DataAnnotationsExtensions;

namespace Appva.Mcss.Web.ViewModels {

    public class SchedulePrintPopOverViewModel {

        public SchedulePrintPopOverViewModel() {
            Template = SchedulePrintTemplate.Schema;
            OnNeedBasis = true;
            StandardSequneces = true;
        }

        public Guid Id { get; set; }

        public Guid ScheduleSettingsId { get; set; }

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

        [Required]
        public SchedulePrintTemplate Template { get; set;}

        [DisplayName("Inkludera vid-behov-läkemedel")]
        public bool OnNeedBasis { get; set; }

        [DisplayName("Inkludera stående ordinationer")]
        public bool StandardSequneces { get; set; }

    }

    public enum SchedulePrintTemplate { 
        Table,
        Schema
    }

}