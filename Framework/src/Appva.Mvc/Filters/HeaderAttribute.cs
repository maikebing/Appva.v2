// <copyright file="HeaderAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Filters
{
    #region Imports.

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Represents an attribute that is used to add HTTP Headers to a Controller Action 
    /// response.
    /// </summary>
    public sealed class HeaderAttribute : ActionFilterAttribute
    {
        #region Variabels.

        /// <summary>
        /// The HTTP header name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The HTTP header value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderAttribute"/> class.
        /// </summary>
        /// <param name="name">The HTTP header name.</param>
        /// <param name="value">The HTTP header value.</param>
        public HeaderAttribute(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader(this.name, this.value);
            base.OnResultExecuted(filterContext);
        }

        #endregion
    }
}