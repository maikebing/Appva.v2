// <copyright file="EmptyJsonResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using Resources;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EmptyJsonResult : ActionResult
    {
        #region Variables.

        /// <summary>
        /// The value to format as JSON.
        /// </summary>
        private readonly object value;

        /// <summary>
        /// The <see cref="JsonRequestBehavior"/>.
        /// </summary>
        private readonly JsonRequestBehavior behavior;

        /// <summary>
        /// The <see cref="JsonSerializerSettings"/> to be used by the formatter.
        /// </summary>
        [NotNull] 
        private readonly JsonSerializerSettings serializerSettings;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyJsonResult"/> class.
        /// </summary>
        public EmptyJsonResult()
            : this(null, JsonRequestBehavior.AllowGet)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyJsonResult"/> class.
        /// </summary>
        /// <param name="behavior">The json request behavior</param>
        /// <param name="serializerSettings">
        /// The <see cref="JsonSerializerSettings"/> to be used by the formatter
        /// </param>
        public EmptyJsonResult(JsonRequestBehavior behavior, JsonSerializerSettings serializerSettings = null)
            : this(null, behavior, serializerSettings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyJsonResult"/> class with the 
        /// given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to format as JSON</param>
        /// <param name="behavior">The json request behavior</param>
        /// <param name="serializerSettings">
        /// The <see cref="JsonSerializerSettings"/> to be used by the formatter
        /// </param>
        public EmptyJsonResult(object value, JsonRequestBehavior behavior, JsonSerializerSettings serializerSettings = null)
        {
            this.value = value;
            this.behavior = behavior;
            this.serializerSettings = serializerSettings ?? new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Error
            };
        }

        #endregion

        #region ActionResult Overrides.

        /// <inheritdoc />
        public override void ExecuteResult([NotNull] ControllerContext context)
        {
            if (this.behavior.Equals(JsonRequestBehavior.DenyGet) &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }
            var response = context.HttpContext.Response;
            response.ContentType = MediaTypes.Json;
            if (this.value != null)
            {
                response.Write(JsonConvert.SerializeObject(this.value, this.serializerSettings));
                return;
            }
            var model = context.Controller.ViewData.Model;
            if (model == null)
            {
                return;
            }
            response.Write(JsonConvert.SerializeObject(model, this.serializerSettings));
        }

        #endregion
    }
}