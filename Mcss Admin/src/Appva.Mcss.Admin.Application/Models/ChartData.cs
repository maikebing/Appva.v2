// <copyright file="ChartData.cs" company="Appva AB">
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
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ChartData
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ChartData(double onTime, double notOnTime)
        {
            this.OnTime = onTime;
            this.NotOnTime = notOnTime;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// 
        /// </summary>
        public double OnTime
        {
            get;
            set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public double NotOnTime
        {
            get;
            set;
        }

        #endregion
    }
}