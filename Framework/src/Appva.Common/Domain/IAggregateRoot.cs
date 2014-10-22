// <copyright file="IAggregateRoot.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Common.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A collection of objects that are bound together by a root entity, 
    /// otherwise known as an aggregate root. The aggregate root guarantees 
    /// the consistency of changes being made within the aggregate by 
    /// forbidding external objects from holding references to its members.
    /// </summary>
    public interface IAggregateRoot
    {
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
    }
}