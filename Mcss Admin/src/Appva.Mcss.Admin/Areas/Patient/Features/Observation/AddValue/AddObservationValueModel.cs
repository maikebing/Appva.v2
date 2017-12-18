// <copyright file="AddObservationValueModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
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
        [IsValidValue()]
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
        /// Gets or sets the scale.
        /// </summary>
        public string Scale
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> ValueSelectList
        {
            get
            {
                switch (this.Scale)
                {
                    case "FecesObservation": return Enum.GetValues(typeof(FecesScale)).Cast<FecesScale>().Select(x => new SelectListItem { Text = FecesUnit.ToString(x), Value = x.ToString() });
                    case "BristolStoolScaleObservation": return Enum.GetValues(typeof(BristolScale)).Cast<BristolScale>().Select(x => new SelectListItem { Text = BristolUnit.ToString(x), Value = x.ToString() });
                    default: return null;
                }
            }
        }

        #endregion
    }
    //// UNRESOLVED: refactor this for a more suitable solution

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class IsValidValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //// UNRESOLVED: work around this. Model needs to know Observation and from there make a proper validation.
            var value_to_test = value.ToString();
            //if (( BristolUnit.HasValidValue(value_to_test) || FecesUnit.HasValidValue(value_to_test) || WeightUnit.HasValidValue(value_to_test)) == false)
            //{
            //    return false;
            //}
            return true;
        }
    }
}