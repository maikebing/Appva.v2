using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using DataAnnotationsExtensions;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mvc;
using Appva.Mcss.Admin.Application.Models;

namespace Appva.Mcss.Web.ViewModels {
    
    public class PrintViewModel {

        public bool EmptySchema { get; set; }
        public Patient Patient { get; set; }
        public ScheduleSettings Schedule { get; set; }
        public IList<Taxon> StatusTaxons { get; set; }

        [Required]
        [Date]
        [DateLessThan(Target = "To")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Från datum")]
        public DateTime From { get; set; }

        [Required]
        [Date]
        [DateGreaterThan(Target = "From")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Till datum")]
        public DateTime To { get; set; }

        public PrintSchedule PrintSchedule { get; set; }

    }

}