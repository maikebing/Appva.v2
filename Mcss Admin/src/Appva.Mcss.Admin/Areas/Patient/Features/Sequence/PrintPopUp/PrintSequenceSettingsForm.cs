// <copyright file="PrintSequenceSettingsForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;
    using Appva.Mvc;
    using DataAnnotationsExtensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PrintSequenceSettingsForm : IRequest<PrintSequence>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSequenceSettingsForm"/> class.
        /// </summary>
        public PrintSequenceSettingsForm()
        {
            OnNeedBasis = true;
            StandardSequneces = true;
        }

        #endregion

        #region Properties.

        public Guid Id { get; set; }

        public Guid ScheduleId { get; set; }

        [Required]
        [Date]
        [DateLessThan(Target = "EndDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [Display(Name = "Från_datum", ResourceType = typeof(Resources.Language))]
        public DateTime StartDate { get; set; }

        [Required]
        [Date]
        [DateGreaterThan(Target = "StartDate")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [Display(Name = "Till_datum", ResourceType = typeof(Resources.Language))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Inkludera_vid_behov_läkemedel", ResourceType = typeof(Resources.Language))]
        public bool OnNeedBasis { get; set; }

        [Display(Name = "Inkludera_stående_ordinationer", ResourceType = typeof(Resources.Language))]
        public bool StandardSequneces { get; set;
        }

        #endregion
    }
}