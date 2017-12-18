// <copyright file="ArbitraryValueExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// <see cref="ArbitraryValue"/> helper extensions.
    /// </summary>
    internal static class ArbitraryValueExtensions
    {
        /// <summary>
        /// Returns the signed data drom the <see cref="ArbitraryValue"/>.
        /// </summary>
        /// <param name="value">The instance.</param>
        /// <returns>A new <see cref="SignedData"/> instance.</returns>
        public static SignedData ToSignedData(this ArbitraryValue value)
        {
            return SignedData.New(value);
        }
    }
}