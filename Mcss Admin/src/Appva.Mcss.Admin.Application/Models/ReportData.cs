// <copyright file="ReportData.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ReportData
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportData"/> class.
        /// </summary>
        public ReportData()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportData"/> class.
        /// </summary>
        /// <param name="date">TODO: what is date?</param>
        /// <param name="onTime">TODO: what is onTime?</param>
        /// <param name="notOnTime">TODO: what is notOnTime?</param>
        public ReportData(DateTime date, double onTime, double notOnTime)
        {
            this.OnTime = onTime;
            this.NotOnTime = notOnTime;
            this.Date = date;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// TODO: Complete summary for Date? Should this be public set?
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Complete summary for OnTime? Should this be public set?
        /// </summary>
        public double OnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Complete summary for NotOnTime? Should this be public set?
        /// </summary>
        public double NotOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Complete summary for Signed? Should this be public set?
        /// </summary>
        public double Signed
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Complete summary for Total? Should this be public set?
        /// </summary>
        public double Total
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Complete summary for OnTimePercentage? 
        /// </summary>
        public double OnTimePercentage
        {
            get
            {
                return this.Total == 0 ? 0 : Math.Round((this.OnTime / this.Total) * 100, 0);
            }
        }

        /// <summary>
        /// TODO: Complete summary for NotOnTimePercentage? 
        /// </summary>
        public double NotOnTimePercentage
        {
            get
            {
                return this.Total == 0 ? 0 : Math.Round((this.NotOnTime / this.Total) * 100, 0);
            }
        }

        /// <summary>
        /// TODO: Complete summary for SignedTaskPercentage? 
        /// </summary>
        public double SignedTaskPercentage
        {
            get
            {
                return this.Total == 0 ? 0 : Math.Round((this.Signed / this.Total) * 100, 0);
            }
        }

        /// <summary>
        /// TODO: Complete summary for PreviousPeriod? Should this be public set?
        /// </summary>
        public ReportData PreviousPeriod
        { 
            get;
            set; 
        }

        /// <summary>
        /// TODO: Complete summary for AverageMinutesDelayed? Should this be public set?
        /// </summary>
        public double AverageMinutesDelayed
        {
            get;
            set;
        }

        #endregion
    }
}