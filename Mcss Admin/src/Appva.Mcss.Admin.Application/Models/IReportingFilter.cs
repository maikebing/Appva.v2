// <copyright file="IReportingFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using NHibernate;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IReportingFilter
    {
        /// <summary>
        /// Filters a query.
        /// </summary>
        /// <param name="query">The query to be filtered</param>
        void Filter(IQueryOver<Task, Task> query);
    }
}