// <copyright file="ExceptionMail.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ExceptionMail
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMail"/> class.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <param name="user">Optional user data</param>
        /// <param name="request">Optional request data</param>
        /// <param name="extraData">Optional extra data</param>
        private ExceptionMail(Exception exception, ExceptionUser user = null, ExceptionRequest request = null, dynamic extraData = null)
        {
            this.Exception = exception;
            this.User = user;
            this.Request = request;
            this.ExtraData = extraData;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The <see cref="Exception"/>.
        /// </summary>
        public Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="ExceptionUser"/>.
        /// </summary>
        public ExceptionUser User
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="ExceptionRequest"/>.
        /// </summary>
        public ExceptionRequest Request
        {
            get;
            private set;
        }

        /// <summary>
        /// The extra data.
        /// </summary>
        public dynamic ExtraData
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ExceptionMail"/> class.
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <param name="user">Optional user data</param>
        /// <param name="request">Optional request data</param>
        /// <param name="extraData">Optional extra data</param>
        /// <returns>A new instance of <see cref="ExceptionMail"/></returns>
        public static ExceptionMail CreateNew(Exception exception, ExceptionUser user = null, ExceptionRequest request = null, dynamic extraData = null)
        {
            return new ExceptionMail(exception, user, request, extraData);
        }

        #endregion
    }
}