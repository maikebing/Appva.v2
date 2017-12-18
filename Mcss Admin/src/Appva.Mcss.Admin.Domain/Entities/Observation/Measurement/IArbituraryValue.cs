// <copyright file="IMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.ComponentModel;

    #endregion

    /// <summary>
    /// Represents the measured value, annotation, quantity or ratio for a 
    /// <see cref="ObservationItem"/>.
    /// </summary>
    public interface IArbituraryValue : IFormattable, IDeepCopyable<IArbituraryValue>
    {
        /// <summary>
        /// The measured value, quantity or ratio.
        /// </summary>
        object Value
        {
            get;
        }

        /// <summary>
        /// The unit of measure, if applied.
        /// </summary>
        IUnitOfMeasurement Unit
        {
            get;
        }

        /// <summary>
        /// The scale of measurement / level of measurement.
        /// </summary>
        LevelOfMeasurement TypeOfScale
        {
            get;
        }
    }
}