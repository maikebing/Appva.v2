// <copyright file="IIdentity.cs" company="Appva AB">
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
    /// <see cref="IEntity"/> identity.
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// The identity.
        /// </summary>
        Guid Id
        {
            get;
        }
    }
}
