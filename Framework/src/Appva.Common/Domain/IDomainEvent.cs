// <copyright file="IDomainEvent.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A domain object that defines an event. Where a domain event is something 
    /// that happened that domain experts care about.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// The domain event version.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// The date time when the event occured at.
        /// </summary>
        DateTime Occurred { get; }
    }
}