using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Models;
using Appva.Mcss.Web.ViewModels;

namespace Appva.Mcss.Web.ViewModels {

    public class ListPatientModel : SearchViewModel<PatientViewModel>
    {
        public bool IsActive { get; set; }
        public bool IsDeceased { get; set; }
    }

}