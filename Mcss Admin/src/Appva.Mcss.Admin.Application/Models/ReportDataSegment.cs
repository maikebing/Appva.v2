// <copyright file="ReportDataSegment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.alvegard@appva.se">Richard Alvegard</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ReportDataSegment
    {
        #region Variables.

        /// <summary>
        /// TODO: what is averageMinutesDelayed?
        /// </summary>
        private double averageMinutesDelayed;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDataSegment"/> class.
        /// </summary>
        public ReportDataSegment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDataSegment"/> class.
        /// </summary>
        /// <param name="date">TODO: what is date?</param>
        /// <param name="onTime">TODO: what is onTime?</param>
        /// <param name="notOnTime">TODO: what is notOnTime?</param>
        public ReportDataSegment(DateTime date, double onTime, double notOnTime)
        {
            this.OnTime = onTime;
            this.NotOnTime = notOnTime;
            this.Date = date;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// TODO: Summary for Date. Should this have public setter?
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Summary for OnTime. Should this have public setter?
        /// </summary>
        public double OnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Summary for NotOnTime. Should this have public setter?
        /// </summary>
        public double NotOnTime
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Summary for Total. Should this have public setter?
        /// </summary>
        public double Total
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Summary for Signed. Should this have public setter?
        /// </summary>
        public double Signed
        {
            get;
            set;
        }

        /// <summary>
        /// TODO: Summary for OnTimePercentage.
        /// </summary>
        public double OnTimePercentage
        {
            get
            {
                return this.Total == 0 ? 0 : Math.Round((this.OnTime / this.Total) * 100, 0);
            }
        }

        /// <summary>
        /// TODO: Summary for NotOnTimePercentage.
        /// </summary>
        public double NotOnTimePercentage
        {
            get
            {
                return this.Total == 0 ? 0 : Math.Round((this.NotOnTime / this.Total) * 100, 0);
            }
        }

        /// <summary>
        /// TODO: Summary for AverageMinutesDelayed.
        /// </summary>
        public double AverageMinutesDelayed
        {
            get
            {
                return this.averageMinutesDelayed != 0 ? this.averageMinutesDelayed / this.Total : 0;
            }

            set
            {
                this.averageMinutesDelayed = value;
            }
        }

        #endregion
    }
}