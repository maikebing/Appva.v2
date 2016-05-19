// <copyright file="LogModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LogModel
    {
        #region Properties.

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id 
        {
            get;
            set;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public LogType Type
        {
            get;
            set;
        }

        /// <summary>
        /// The system.
        /// </summary>
        public SystemType System
        {
            get;
            set;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// The route.
        /// </summary>
        public string Route
        {
            get;
            set;
        }

        /// <summary>
        /// The IP address.
        /// </summary>
        public string IpAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The user account.
        /// </summary>
        public string AccountName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient.
        /// </summary>
        public string PatientName
        {
            get;
            set;
        }

        /// <summary>
        /// The version
        /// </summary>
        public int Version
        {
            get;
            set;
        }

        /// <summary>
        /// The created date
        /// </summary>
        public DateTime CreatedAt
        {
            get;
            set;
        }
    }
         

        #endregion
    

}