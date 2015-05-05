// <copyright file="ListCalendar.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListCalendar : IRequest<EventListViewModel>
    {
        /// <summary>
        /// The patient ID.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Optional to use previous month, or previous month of date.
        /// </summary>
        public string Prev
        {
            get;
            set;
        }

        /// <summary>
        /// Optional to use next month, or next month of date.
        /// </summary>
        public string Next
        {
            get;
            set;
        }

        /// <summary>
        /// Optional current date used with prev/next.
        /// </summary>
        public DateTime? Date
        {
            get;
            set;
        }

        /// <summary>
        /// Optional filter list.
        /// </summary>
        public string[] Filter
        {
            get;
            set;
        }
    }
}