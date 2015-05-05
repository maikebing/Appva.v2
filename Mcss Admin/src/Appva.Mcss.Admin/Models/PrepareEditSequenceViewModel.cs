using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Web.ViewModels
{
    public class PrepareEditSequenceViewModel : Identity<SchemaPreparation>
    {
        /// <summary>
        /// The <c>PreparedSequence</c> ID.
        /// </summary>
        public Guid PreparedSequenceId
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Namn på läkemedlet måste anges.")]
        [DisplayName("Ny rad")]
        public string Name { get; set; }
    }
}