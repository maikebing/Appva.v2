// <copyright file="CreateScheduleForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;
    using Appva.Mcss.Admin.Models;
    using Appva.Cqrs;

    #endregion

    public class CreateScheduleForm : IRequest<ListSchedule>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSchedulePublisher"/> class.
        /// </summary>
        public CreateScheduleForm()
        {
            Items = new List<SelectListItem>();
        }

        #endregion

        /// <summary>
        /// Patient ID.
        /// </summary>
        public virtual Guid Id
        {
            get;
            set;
        }

        [Required(ErrorMessageResourceName = "En_lista_måste_väljas", ErrorMessageResourceType = typeof(Resources.Language))]
        [Display(Name = "Typ_av_lista", ResourceType = typeof(Resources.Language))]
        [Remote("VerifyUnique", "Schedule", HttpMethod = "POST", ErrorMessageResourceName = "Denna_lista_finns_sedan_tidigare_inlagd", ErrorMessageResourceType = typeof(Resources.Language))]
        public virtual Guid ScheduleSetting
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> Items
        {
            get;
            set;
        }

    }

}
