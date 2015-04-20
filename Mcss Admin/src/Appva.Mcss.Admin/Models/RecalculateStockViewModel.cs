using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class RecalculateStockViewModel {

        public Sequence Sequence { get; set; }

        [Required]
        [Range(0, 9999)]
        [DisplayName("Ange räknat antal")]
        public int Amount { get; set; }

    }

}