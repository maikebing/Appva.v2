// <copyright file="Log.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring.Domain.Entities
{
    #region Imports.

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Database HTTP log entity.
    /// </summary>
    public class Log : Entity<Log>, IAggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        public Log(string application, string machineName, string tenant, string userName, int httpStatusCode)
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Application = application;
            this.MachineName = machineName;
            this.Tenant = tenant;
            this.UserName = userName;
            this.HttpStatusCode = httpStatusCode;
            this.HttpStatusDefinition = "";
            this.RequestData = "";
            this.IsException = false;
            this.ExceptionType = "";
            this.ExceptionSource = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Log()
        {
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public virtual DateTime CreatedAt
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public virtual DateTime UpdatedAt
        {
            get;
            protected set;
        }

        /// <summary>
        /// The application name of the sender.
        /// </summary>
        public virtual string Application 
        { 
            get;
            protected set; 
        }

        /// <summary>
        /// The machine name of the sender.
        /// </summary>
        public virtual string MachineName 
        { 
            get;
            protected set; 
        }

        /// <summary>
        /// The tenant name for the HTTP request.
        /// </summary>
        public virtual string Tenant 
        { 
            get;
            protected set; 
        }

        /// <summary>
        /// The current user name.
        /// </summary>
        public virtual string UserName
        {
            get;
            protected set;
        }

        /// <summary>
        /// The HTTP status code.
        /// See <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html">Status codes</a>.
        /// </summary>
        public virtual int HttpStatusCode
        {
            get;
            protected set;
        }

        /// <summary>
        /// The HTTP request status code definition in plain text.
        /// See <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html">Status codes</a>.
        /// </summary>
        public virtual string HttpStatusDefinition 
        { 
            get;
            protected set; 
        }

        /// <summary>
        /// The request data.
        /// </summary>
        public virtual string RequestData 
        { 
            get; 
            protected set; 
        }

        /// <summary>
        /// Whether or not the request raised an exception.
        /// </summary>
        public virtual bool IsException
        {
            get;
            protected set;
        }

        /// <summary>
        /// The type, e.g. error type.
        /// </summary>
        public virtual string ExceptionType
        {
            get;
            protected set;
        }

        /// <summary>
        /// The exception source.
        /// </summary>
        public virtual string ExceptionSource
        {
            get;
            protected set;
        }
        #endregion
    }
}