// <copyright file="ObservationRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    #endregion

    public class AddMeasurementValueModel : Identity<ViewMeasurementModel>
    {
        #region Variables

        [Required]
        public Guid MeasurementId { get; set; }

        public string Name { get; set; }

        public string Instruction { get; set; }

        [Required]
        public string Value { get; set; }

        public string Comment { get; set; }

        public string Unit { get; set; }

        public string Scale { get; set; }

        public string LongScale { get; set; }

        #endregion
    }
}