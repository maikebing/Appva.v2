// <copyright file="ExceptionFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Core.Exceptions;

    #endregion

    /// <summary>
    /// Marker interface for exception handling for web. 
    /// </summary>
    public interface IWebExceptionHandler : IExceptionHandler
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ExceptionFilter : IExceptionFilter
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter"/> class.
        /// </summary>
        /// <param name="handler">The <see cref="IWebExceptionHandler"/></param>
        public ExceptionFilter(IWebExceptionHandler handler)
        {
            this.Handler = handler;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The exception handler.
        /// </summary>
        public IWebExceptionHandler Handler
        {
            get;
            set;
        }

        #endregion

        #region IExceptionFilter Members.

        /// <inheritdoc />
        public void OnException(ExceptionContext filterContext)
        {
            this.Handler.Handle(filterContext.Exception);
        }

        #endregion
    }
}