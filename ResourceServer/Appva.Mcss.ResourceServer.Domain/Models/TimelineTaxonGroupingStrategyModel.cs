// <copyright file="TimelineTaxonGroupingStrategyModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Slot model.
    /// </summary>
    /// <example>
    /// Example response:
    /// {
    ///    "slot_id": "",
    ///    "date_time_start": "2014-01-01T0900",
    ///    "date_time_end": "2014-01-01T0930",
    ///    "is_all_day": "false",
    ///    "has_incomplete_tasks": "true"
    ///    "categories": ["Medication"],
    ///    "patient": {
    ///        "id": "abc",
    ///        "full_name": "Clint Eastwood",
    ///        "has_incomplete_tasks": "true"
    ///    }
    /// }
    /// </example>
    public class TimelineTaxonGroupingStrategyModel : IGroupingStrategy
    {
        /// <summary>
        /// TODO: DateTimeScheduled.
        /// </summary>
        public string DateTimeScheduled 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: DateTimeStart.
        /// </summary>
        public string DateTimeStart 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: DateTimeEnd.
        /// </summary>
        public string DateTimeEnd
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: IsAllDay.
        /// </summary>
        public bool IsAllDay 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: HasIncompleteTasks.
        /// </summary>
        public bool HasIncompleteTasks 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Categories.
        /// </summary>
        public List<string> Categories 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: TypeId.
        /// </summary>
        public string TypeId 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Patient.
        /// </summary>
        public dynamic Patient 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Completed.
        /// </summary>
        public CompletedDetailsModel Completed 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// TODO: Sequence.
        /// </summary>
        public Guid Sequence 
        { 
            get; 
            set; 
        }
    }
}