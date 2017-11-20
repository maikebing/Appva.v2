// <copyright file="IPaged.cs" company="Appva AB">
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
    public interface IPaged
    {
        /// <summary>
        /// The page query parameters.
        /// </summary>
        IPageQuery PageQuery
        {
            get;
        }

        /// <summary>
        /// The paged result set collection item count.
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// The total count.
        /// </summary>
        long TotalCount
        {
            get;
        }

        /// <summary>
        /// Whether or not there are any results.
        /// </summary>
        bool HasResults
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IPaged<T> : IPaged where T : class
    {
        /// <summary>
        /// The paged result set.
        /// </summary>
        IEnumerable<T> Items
        {
            get;
        }
    }
}