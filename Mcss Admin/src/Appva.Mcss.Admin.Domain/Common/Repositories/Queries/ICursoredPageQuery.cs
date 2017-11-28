// <copyright file="ICursoredPageQuery.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface ICursoredPageQuery
    {
        /// <summary>
        /// Specific date positioning in the result set.
        /// </summary>
        /// <remarks>Not an actual database cursor.</remarks>
        DateTime? Cursor
        {
            get;
        }

        /// <summary>
        /// Updates the cursor.
        /// </summary>
        /// <param name="cursor">The new cursor.</param>
        void Update(DateTime? cursor);
    }
}