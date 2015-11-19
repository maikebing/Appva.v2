// <copyright file="CalendarTask.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CalendarTask
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The event starttime
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// The event endtime
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// The category name
        /// </summary>
        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// The category name
        /// </summary>
        public Guid CategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The event description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The category color
        /// </summary>
        public string Color 
        {
            get;
            set;
        }

        /// <summary>
        /// If activity is full day 
        /// </summary>
        public bool IsFullDayEvent 
        {
            get;
            set; 
        }

        /// <summary>
        /// If task exist, the task id
        /// </summary>
        public Guid TaskId
        {
            get; 
            set; 
        }

        /// <summary>
        /// If activity need quittance
        /// </summary>
        public bool NeedsQuittance 
        { 
            get;
            set; 
        }

        /// <summary>
        /// If the activity is quittanced
        /// </summary>
        public bool IsQuittanced 
        { 
            get;
            set;
        }

        /// <summary>
        /// The Account which quittanced the activity
        /// </summary>
        public Account QuittancedBy 
        {
            get;
            set;
        }

        /// <summary>
        /// If activity is signed the <see cref="SignatureModel"/>
        /// </summary>
        public SignatureModel Signature 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If the event should be signed in mobile device
        /// </summary>
        public bool NeedsSignature 
        { 
            get;
            set; 
        }

        /// <summary>
        /// The interval, e.g. weekly monthly or yearly
        /// </summary>
        public int Interval 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The intervall factor, e.g. every, every second etc.
        /// </summary>
        public int IntervalFactor 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// If the event should be repeated on given day (e.g. "Monday")
        /// </summary>
        public bool RepeatAtGivenDay 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Returns the the repition in natural language
        /// </summary>
        public string RepetionText
        {
            get
            {
                if (this.Interval == 7)
                {
                    return string.Format("{1:dddd} {2} vecka",
                        ((this.StartTime.Day - (this.StartTime.Day % 7)) / 7) + 1,
                        this.StartTime,
                        NumberToNaturalLanguage(this.IntervalFactor)
                        ).FirstToUpper();
                }
                else if (this.RepeatAtGivenDay)
                {
                    return string.Format("Den {0} {1:dddd}en {2} månad",
                        ((this.StartTime.Day - (this.StartTime.Day % 7)) / 7) + 1,
                        this.StartTime,
                        NumberToNaturalLanguage(this.IntervalFactor)
                        );
                }
                else
                {
                    return string.Format("Den {0:d} {1} månad",
                        this.StartTime,
                        NumberToNaturalLanguage(this.IntervalFactor)
                        );
                }
            }
        }

        /// <summary>
        /// The patient id
        /// </summary>
        public Guid PatientId { get; set; }
             
        #endregion

        #region Static Helpers

        private static string NumberToNaturalLanguage(int n)
        {
            if (n <= 1)
            {
                return "varje";
            }
            else if (n == 2)
            {
                return "varannan";
            }
            return string.Format("{0}:e", n);
        } 

        #endregion 

    }
}