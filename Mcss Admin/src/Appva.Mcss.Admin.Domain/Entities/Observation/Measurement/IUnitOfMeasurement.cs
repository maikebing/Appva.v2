// <copyright file="IUnitOfMeasurement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IUnitOfMeasurement
    {
        /// <summary>
        /// The official name of the unit.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The official symbol used in print.
        /// </summary>
        string Symbol
        {
            get;
        }
    }
}