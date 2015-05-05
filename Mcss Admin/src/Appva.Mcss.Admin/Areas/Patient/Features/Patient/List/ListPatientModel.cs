using System;
using System.Collections.Generic;
using Appva.Mcss.Web.ViewModels;

namespace Appva.Mcss.Web.ViewModels {

    public class ListPatientModel
    {
        public SearchViewModel<PatientViewModel> Search { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }
    }

}