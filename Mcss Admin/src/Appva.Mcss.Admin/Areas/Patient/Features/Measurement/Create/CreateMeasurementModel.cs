using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appva.Mcss.Admin.Models
{
    public class CreateMeasurementModel : Identity<ListMeasurementModel>
    {
        public new Guid Id { get; set; }

        [DisplayName("Namn")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Instruktion")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Enhet")]
        public IEnumerable<SelectListItem> SelectUnitList
        {
            get;
            set;
        }

        [DisplayName("Kräver delegering för")]
        public IEnumerable<SelectListItem> SelectDelegationList
        {
            get;
            set;
        }

        [Required]
        public string SelectedUnit { get; set; }

        [Required]
        public string SelectedDelegation { get; set; }
    }
}