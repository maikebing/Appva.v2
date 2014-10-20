﻿// <copyright file="ExceptionFilterAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Application.ExceptionHandling
{
    #region Imports.

    using System.Web.Http.Filters;
    using Appva.Core.Extensions;
    using Autofac.Integration.WebApi;
    using Common.Logging;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ExceptionFilterAttribute : IAutofacExceptionFilter
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> for the <see cref="ExceptionFilterAttribute"/>.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger<ExceptionFilterAttribute>();

        #endregion

        #region IAutofacExceptionFilter Members.

        /// <inheritdoc />
        public void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            if (exception.IsNotNull())
            {
                Log.Error("An exception occured!", exception);
            }
        }

        #endregion
    }
}