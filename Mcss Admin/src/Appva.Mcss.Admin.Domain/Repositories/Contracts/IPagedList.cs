// <copyright file="IPagedList.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IPagedList<T> where T : Entity<T>
    {
        /// <summary>
        /// The current page number.
        /// </summary>
        long PageNumber
        {
            get;
        }

        /// <summary>
        /// The number of items to be returned.
        /// </summary>
        long PageSize
        {
            get;
        }

        /// <summary>
        /// The next page number.
        /// </summary>
        long NextPage
        {
            get;
        }

        /// <summary>
        /// The total size of items.
        /// </summary>
        long TotalSize
        {
            get;
        }

        /// <summary>
        /// The filtered set of entities.
        /// </summary>
        IList<T> Items
        {
            get;
        }
    }
}