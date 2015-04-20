using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mvc.Html.DataAnnotations;

namespace Appva.Mcss.Web.ViewModels {
    
    public class PatientViewModel {

        public virtual Guid Id { get; set; }

        public bool Active { get; set; }

        public bool HasUnattendedTasks { get; set; }

        public string FullName { get; set; }

        public string UniqueIdentifier { get; set; }
        
        public string Address { get; set; }

        public string FirstLineContact { get; set; }

        public string Overseeing { get; set; }

        public string Superior { get; set; }

        public IList<Taxon> SeniorAlerts { get; set; }

    }

}