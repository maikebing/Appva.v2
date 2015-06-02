// <copyright file="IEntity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// An object that is not defined by its attributes, but rather by a thread of 
    /// continuity and its identity.
    /// </summary>
    public interface IEntity : IIdentity
    {
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
        /// The version.
        /// </summary>
        int Version
        {
            get;
        }
    }
}
