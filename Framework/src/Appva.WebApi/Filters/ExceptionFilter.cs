// <copyright file="ExceptionFilter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.WebApi.Filters
{
    #region Imports.

    using System.Web.Http.Filters;

    #endregion

    /// <summary>
    /// Marker interface for exception handling for web api. 
    /// </summary>
    public interface IWebApiExceptionHandler : Core.Exceptions.IExceptionHandler
    {
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter"/> class.
        /// </summary>
        /// <param name="handler">The <see cref="IWebApiExceptionHandler"/></param>
        public ExceptionFilter(IWebApiExceptionHandler handler)
        {
            this.Handler = handler;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The exception handler.
        /// </summary>
        public IWebApiExceptionHandler Handler
        {
            get;
            set;
        }

        #endregion

        #region ExceptionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnException(HttpActionExecutedContext context)
        {
            this.Handler.Handle(context.Exception);
        }

        #endregion
    }
}