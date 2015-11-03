using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Web.ViewModels
{
    public class InventoryTransactionItemViewModel
    {
        public string InventoryName { get; set; }
        [Required]
        public Guid InventoryId { get; set; }

        public Guid SequenceId { get; set; }

        [Required]
        public string Operation { get; set; }

        [Display(Name="Antal")]
        public decimal Value { get; set; }

        public Guid TaskId { get; set; }

        [Display(Name = "Notis")]
        public string Description { get; set; }

        public string ReturnUrl { get; set; }
    }
}