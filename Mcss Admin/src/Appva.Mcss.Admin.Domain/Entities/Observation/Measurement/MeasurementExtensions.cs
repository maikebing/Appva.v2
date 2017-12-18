// <copyright file="MeasurementExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// <see cref="ArbituraryMeasuredValue"/> helper extensions.
    /// </summary>
    internal static class MeasurementExtensions
    {
        /// <summary>
        /// Returns the signed data drom the <see cref="IArbituraryValue"/>.
        /// </summary>
        /// <param name="measurement">The instance.</param>
        /// <returns>A new <see cref="SignedData"/> instance.</returns>
        public static SignedData ToSignedData(this IArbituraryValue measurement)
        {
            return SignedData.New(measurement);
        }
    }
}