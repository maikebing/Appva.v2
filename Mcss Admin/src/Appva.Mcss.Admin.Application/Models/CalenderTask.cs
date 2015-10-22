// <copyright file="CalenderTask.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CalenderTask
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalenderTask"/> class.
        /// </summary>
        public CalenderTask()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The calendar id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The starttime for the task
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// The endtime of the task
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the category
        /// </summary>
        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// The description of the task
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion
    }
}