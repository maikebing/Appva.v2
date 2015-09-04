// <copyright file="ChartData.cs" company="Appva AB">
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
    public class ReportDataSegment
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private double averageMinutesDelayed;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ReportDataSegment()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChartData"/> class.
        /// </summary>
        public ReportDataSegment(DateTime date, double onTime, double notOnTime)
        {
            this.OnTime = onTime;
            this.NotOnTime = notOnTime;
            this.Date = date;
        }

        #endregion

        #region Properties.

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

        public double Total
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Signed
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public double OnTimePercentage
        {
            get { return this.Total == 0 ? 0 : Math.Round((this.OnTime / this.Total) * 100, 0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double NotOnTimePercentage
        {
            get { return this.Total == 0 ? 0 : Math.Round((this.NotOnTime / this.Total) * 100, 0); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double AverageMinutesDelayed 
        {
            get { return this.averageMinutesDelayed != 0 ? this.averageMinutesDelayed / this.Total : 0; }
            set { this.averageMinutesDelayed = value; } 
        }

        #endregion
    }
}