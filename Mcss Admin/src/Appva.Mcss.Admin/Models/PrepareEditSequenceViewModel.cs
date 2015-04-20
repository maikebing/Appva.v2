using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Appva.Mcss.Web.ViewModels
{
    public class PrepareEditSequenceViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Namn på läkemedlet måste anges.")]
        [DisplayName("Ny rad")]
        public string Name { get; set; }
    }
}