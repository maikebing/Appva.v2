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

        [Required(ErrorMessage = "En lista måste väljas.")]
        [DisplayName("Typ av lista")]
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
