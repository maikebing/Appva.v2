// <copyright file="AddMeasurementValueModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports

    using System;
    using System.ComponentModel.DataAnnotations;
    using Appva.Mcss.Domain.Unit;

    #endregion

    /// <summary>
    /// Class AddMeasurementValueModel.
    /// </summary>
    public class AddMeasurementValueModel : Identity<ListMeasurement>
    {
        #region Variables

        /// <summary>
        /// The measurement Id
        /// </summary>
        [Required]
        public Guid MeasurementId
        {
            get;
            set;
        }

        /// <summary>
        /// The name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the instruction.
        /// </summary>
        public string Instruction
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        public string Scale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the long scale.
        /// </summary>
        public string LongScale
        {
            get;
            set;
        }

        public FecesScale CommonScaleValues
        {
            get;
            set;
        }

        public string SelectedScaleValue
        {
            get;
            set;
        }

        #endregion
    }
}