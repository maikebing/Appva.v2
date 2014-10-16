// <copyright file="ValidateAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.WebApi.Filters
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// The 422 (Unprocessable Entity) status code means the server
    /// understands the content type of the request entity (hence a
    /// 415(Unsupported Media Type) status code is inappropriate), and the
    /// syntax of the request entity is correct (thus a 400 (Bad Request)
    /// status code is inappropriate) but was unable to process the contained 
    /// instructions.  For example, this error condition may occur if an XML 
    /// request body contains well-formed (i.e., syntactically correct), but 
    /// semantically erroneous, XML instructions.
    /// <link>https://tools.ietf.org/html/rfc4918</link>
    /// </summary>
    public sealed class ValidateAttribute : ActionFilterAttribute
    {
        #region Variables.

        /// <summary>
        /// The HTTP method.
        /// </summary>
        private readonly HttpMethod httpMethod;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAttribute"/> class.
        /// </summary>
        public ValidateAttribute() : this(HttpMethod.Post)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateAttribute"/> class.
        /// </summary>
        /// <param name="httpMethod">The HTTP method</param>
        public ValidateAttribute(HttpMethod httpMethod)
        {
            this.httpMethod = httpMethod;
        }

        #endregion

        #region Overrides.

        /// <inheritdoc />
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (this.httpMethod.IsNotNull() && context.Request.Method.NotEquals(this.httpMethod))
            {
                return;
            }
            if (! context.ModelState.IsValid)
            {
                var summary = new ValidationSummary("Validation Failed");
                var keys = context.ModelState.Keys;
                foreach (var key in keys)
                {
                    var modelState = context.ModelState[key];
                    foreach (var error in modelState.Errors)
                    {
                        var resource = key.Split('.')[1].ToLowerCaseUnderScore();
                        var field = key.Split('.')[0].ToUpperInvariant();
                        var message = error.ErrorMessage ?? "unknown " + error.Exception.Message;
                        summary.Add(new ValidationError(resource, field, message));
                    }
                }
                context.Response = context.Request.CreateResponse<ValidationSummary>((HttpStatusCode) 422, summary);
            }
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Validation summary with errors.
        /// </summary>
        private sealed class ValidationSummary
        {
            #region Variables.

            /// <summary>
            /// The validation error summary.
            /// </summary>
            private readonly string message;

            /// <summary>
            /// The vallidation errors.
            /// </summary>
            private readonly List<ValidationError> errors;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="ValidationSummary"/> class.
            /// </summary>
            /// <param name="message">The error message</param>
            public ValidationSummary(string message)
            {
                this.message = message;
                this.errors = new List<ValidationError>();
            }

            #endregion

            #region Public Functions.

            /// <summary>
            /// Returns the error message.
            /// </summary>
            public string Message
            {
                get
                {
                    return this.message;
                }
            }

            /// <summary>
            /// Returns the errors.
            /// </summary>
            public List<ValidationError> Errors
            {
                get
                {
                    return this.errors;
                }
            }

            /// <summary>
            /// Adds an error.
            /// </summary>
            /// <param name="error">The <see cref="ValidationError"/></param>
            public void Add(ValidationError error)
            {
                this.errors.Add(error);
            }

            #endregion
        }

        /// <summary>
        /// The validation error.
        /// </summary>
        private sealed class ValidationError
        {
            #region Variabels.

            /// <summary>
            /// The resource.
            /// </summary>
            private readonly string resource;

            /// <summary>
            /// The field.
            /// </summary>
            private readonly string field;

            /// <summary>
            /// The error message.
            /// </summary>
            private readonly string message;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="ValidationError"/> class.
            /// </summary>
            /// <param name="resource">The resource</param>
            /// <param name="field">The field</param>
            /// <param name="message">The error message</param>
            public ValidationError(string resource, string field, string message)
            {
                this.resource = resource;
                this.field = field;
                this.message = message;
            }

            #endregion

            #region Public Properties.

            /// <summary>
            /// Returns the resource.
            /// </summary>
            public string Resource
            {
                get
                {
                    return this.resource;
                }
            }

            /// <summary>
            /// Returns the field.
            /// </summary>
            public string Field
            {
                get
                {
                    return this.field;
                }
            }

            /// <summary>
            /// Returns the error message.
            /// </summary>
            public string Message
            {
                get
                {
                    return this.message;
                }
            }

            #endregion
        }

        #endregion
    }
}