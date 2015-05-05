using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Cqrs;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Web.ViewModels
{
    public class PrepareAddSequenceViewModel : IRequest<SchemaPreparation>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The scheduler ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }

        [Required(ErrorMessage="Namn på läkemedlet måste anges.")]
        [DisplayName("Ny rad")]
        public string Name { get; set; }

    }
}