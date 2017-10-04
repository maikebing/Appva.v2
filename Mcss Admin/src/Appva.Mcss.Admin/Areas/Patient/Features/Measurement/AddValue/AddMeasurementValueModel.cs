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
    using System.Linq;
    using System.Web;

    #endregion

    public class AddMeasurementValueModel : Identity<ViewMeasurementModel>
    {
        #region Variables

        public MeasurementObservation Observation { get; set; }
        public string Value { get; set; }
        public string Comment { get; set; }
        public string Unit { get; set; }

        #endregion
    }
}