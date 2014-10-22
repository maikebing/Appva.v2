// <copyright file="IEntity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// An object that is not defined by its attributes, but rather 
    /// by a thread of continuity and its identity.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        /// The identity.
        /// </summary>
        Guid Id
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