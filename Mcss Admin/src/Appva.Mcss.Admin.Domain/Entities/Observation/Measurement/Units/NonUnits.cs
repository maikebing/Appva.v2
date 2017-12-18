// <copyright file="NonUnits.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Category("non-units")]
    internal static class NonUnits
    {
        /// <summary>
        /// The non unit for classification (consistency) of human feces.
        /// </summary>
        public static readonly UnitOfMeasurement BristolStoolScale = new NonUnitOfMeasurement("Bristol stool scale", "bristol_stool_scale");

        /// <summary>
        /// The non unit for toilet activity.
        /// </summary>
        public static readonly UnitOfMeasurement TenaIdentifiScale = new NonUnitOfMeasurement("Bristol stool scale", "bristol_stool_scale");

        /// <summary>
        /// The non unit for classification (weight) of human feces.
        /// </summary>
        public static readonly UnitOfMeasurement ArbitraryStoolScale = new NonUnitOfMeasurement("Bristol stool scale", "custom_size_stool_scale_sv");
    }
}