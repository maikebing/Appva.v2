using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Appva.Cqrs;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Admin.Models
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

        //[Required(ErrorMessageResourceName = "Namn_på_läkemedlet_måste_anges", ErrorMessageResourceType = typeof(Resources.Language))]
        //[Display(Name = "Ny_rad", ResourceType = typeof(Resources.Language))]
        public string Name { get; set; }

    }
}