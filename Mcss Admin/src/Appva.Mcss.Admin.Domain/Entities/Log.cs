// <copyright file="Log.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Log : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        public Log()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The message.
        /// </summary>
        public virtual LogLevel Level
        {
            get;
            set;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public virtual LogType Type
        {
            get;
            set;
        }

        /// <summary>
        /// The system.
        /// </summary>
        public virtual SystemType System
        {
            get;
            set;
        }

        /// <summary>
        /// The message.
        /// </summary>
        public virtual string Message
        {
            get;
            set;
        }

        /// <summary>
        /// The route.
        /// </summary>
        public virtual string Route
        {
            get;
            set;
        }

        /// <summary>
        /// The IP address.
        /// </summary>
        public virtual string IpAddress
        {
            get;
            set;
        }

        /// <summary>
        /// The user account.
        /// </summary>
        public virtual Account Account
        {
            get;
            set;
        }

        /// <summary>
        /// The patient.
        /// </summary>
        public virtual Patient Patient
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// The Log level.
    /// </summary>
    public enum LogLevel
    {
        Info       = 0,
        Warn       = 1,
        Error      = 2,
        TakeAction = 3
    }

    /// <summary>
    /// The Log type.
    /// </summary>
    public enum LogType
    {
        None           = 0,
        Read           = 1,
        Write          = 2,
        Authentication = 3,
        SignedIn       = 4,
        SignedOut      = 5
    }

    /// <summary>
    /// Used for tenant independant logs.
    /// </summary>
    public enum SystemType
    {
        None   = 0,
        Web    = 1,
        Device = 2,
        Admin  = 3,
        Client = 4
    }
}