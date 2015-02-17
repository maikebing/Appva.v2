// <copyright file="TimelineGroupModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Timeline model.
    /// </summary>
    /// <example>
    /// Response example:
    /// {
    ///    "current_date": "2014-01-02",
    ///    "previous_date": "2014-01-01",
    ///    "next_date": "2014-01-03",
    ///    "grouping_strategy": "..."
    ///    "entities": [
    ///        {
    ///            ...
    ///        }
    ///    ]
    /// }
    /// </example>
    /// <typeparam name="TEntity">The Type</typeparam>
    public class TimelineGroupModel<TEntity>
    {
        /// <summary>
        /// Previous date with tasks to be completed.
        /// </summary>
        public string PreviousDate 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The current date.
        /// </summary>
        public string CurrentDate 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Next date with tasks to be completed.
        /// </summary>
        public string NextDate 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: GroupingStrategy
        /// </summary>
        public string GroupingStrategy 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The collection of items within the span.
        /// </summary>
        public IList<TEntity> Entities 
        { 
            get; 
            set; 
        }
    }
}