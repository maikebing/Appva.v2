﻿// <copyright file="AddObservationValueModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.ComponentModel.DataAnnotations;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Domain.Unit;

    #endregion

    /// <summary>
    /// Class AddObservationValueModel.
    /// </summary>
    public class AddObservationValueModel : Identity<ListObservation>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddObservationValueModel"/> class.
        /// </summary>
        public AddObservationValueModel()
        {
        }

        /// <summary>
        /// Creates an instance of AddObservationValueModel
        /// </summary>
        /// <param name="observation"></param>
        public AddObservationValueModel(Observation observation)
        {
            this.ObservationId = observation.Id;
            this.Name = observation.Name;
            this.Instruction = observation.Description;
            this.Scale = observation.GetType().Name;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The Observation Id
        /// </summary>
        [Required]
        public Guid ObservationId
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

        public FecesScale FecesScaleValues
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