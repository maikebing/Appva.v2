// <copyright file="SecurityHeader.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Security
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// Represents an attribute that is used to add HTTP Headers to a Controller Action response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple = true)]
    public abstract class SecurityHeader : ActionFilterAttribute
    {
        #region Variabels.

        /// <summary>
        /// The HTTP header field.
        /// </summary>
        private readonly string field;

        /// <summary>
        /// The HTTP header value.
        /// </summary>
        private readonly string value;

        #endregion
        
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeader"/> class.
        /// </summary>
        /// <param name="field">The HTTP header field</param>
        /// <param name="value">The HTTP header value</param>
        protected SecurityHeader(string field, string value)
        {
            this.field = field;
            this.value = value;
        }

        #endregion

        #region Protected Members.

        /// <summary>
        /// Returns the default value.
        /// </summary>
        protected abstract string DefaultValue
        {
            get;
        }

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var headerValue = this.value.IsNotEmpty() ? this.value : this.DefaultValue;
            if (headerValue.IsNotEmpty())
            {
                filterContext.HttpContext.Response.AppendHeader(this.field, headerValue);
            }
            base.OnResultExecuted(filterContext);
        }

        #endregion
    }
}