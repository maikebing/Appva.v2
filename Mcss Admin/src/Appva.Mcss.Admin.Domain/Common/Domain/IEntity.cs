// <copyright file="IEntity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Returns the ID.
        /// </summary>
        Guid Id
        {
            get;
        }

        /// <summary>
        /// Whether or not the entity is active or not.
        /// </summary>
        bool IsActive
        {
            get;
        }

        /// <summary>
        /// Entity created at date time.
        /// </summary>
        DateTime CreatedAt
        {
            get;
        }

        /// <summary>
        /// Entity updated at date time.
        /// </summary>
        DateTime UpdatedAt
        {
            get;
        }

        /// <summary>
        /// Updates the <see cref="UpdatedAt"/>.
        /// </summary>
        void MarkAsUpdated();
    }
}