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
    public class ReportData
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ReportData()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ReportData(DateTime date, double onTime, double notOnTime)
        {
            this.OnTime = onTime;
            this.NotOnTime = notOnTime;
            this.Date = date;
        }

        #endregion

        #region Properties.

        /*public DateTime FromDate
        {
            get;
            set;
        }*/

        public DateTime Date
        {
            get;
            set;
        }

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

        public double Signed
        {
            get;
            set;
        }

        public double Total
        {
            get;
            set;
        }

        public double OnTimePercentage
        {
            get { return this.Total == 0 ? 0 : Math.Round((this.OnTime/this.Total)*100,0); }
        }

        public double NotOnTimePercentage
        {
            get { return this.Total == 0 ? 0 : Math.Round((this.NotOnTime / this.Total) * 100,0); }
        }

        public double SignedTaskPercentage
        {
            get { return this.Total == 0 ? 0 : Math.Round((this.Signed / this.Total) * 100, 0); }
        }

        public ReportData PreviousPeriod
        { 
            get;
            set; 
        }

        #endregion

        public double AverageMinutesDelayed { get; set; }
    }
}