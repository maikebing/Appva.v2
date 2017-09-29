using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Models
{
    public class CreateOrUpdateMeasurement : Identity<ListMeasurementModel>
    {
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Instruktion")]
        public string Description { get; set; }

        [DisplayName("Enhet")]
        public IEnumerable<SelectListItem> SelectUnitList
        {
            get;
            set;
        }

        public string SelectedUnit { get; set; }

        [DisplayName("Kräver delegering för")]
        [Required(AllowEmptyStrings = true)]
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }
        [Required(AllowEmptyStrings = true)]
        public string SelectedDelegation { get; set; }
    }
}