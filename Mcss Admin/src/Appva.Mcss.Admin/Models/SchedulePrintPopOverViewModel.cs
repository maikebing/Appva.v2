using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mvc;
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
        [Display(Name = "Från_datum", ResourceType = typeof(Appva.Mcss.Admin.Resources.Language))]
        public DateTime PrintStartDate { get; set; }

        [Required]
        [Date]
        [DateGreaterThan(Target = "PrintStartDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [Display(Name = "Till_datum", ResourceType = typeof(Appva.Mcss.Admin.Resources.Language))]
        public DateTime PrintEndDate { get; set; }

        [Required]
        public SchedulePrintTemplate Template { get; set;}

        [Display(Name = "Inkludera_vid_behov_läkemedel", ResourceType = typeof(Appva.Mcss.Admin.Resources.Language))]
        public bool OnNeedBasis { get; set; }

        [Display(Name = "Inkludera_stående_ordinationer", ResourceType = typeof(Appva.Mcss.Admin.Resources.Language))]
        public bool StandardSequneces { get; set; }

    }

    public enum SchedulePrintTemplate { 
        Table,
        Schema
    }

}