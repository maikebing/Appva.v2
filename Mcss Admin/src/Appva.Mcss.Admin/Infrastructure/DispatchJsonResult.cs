// <copyright file="DispatchJsonResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DispatchJsonResult : ActionResult
    {
        #region Variables.

        /// <summary>
        /// The <see cref="JsonRequestBehavior"/>.
        /// </summary>
        private readonly JsonRequestBehavior behavior;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchJsonResult"/> class.
        /// </summary>
        /// <param name="behavior">The json request behavior</param>
        public DispatchJsonResult(JsonRequestBehavior behavior)
        {
            this.behavior = behavior;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// When set MaxJsonLength passed to the JavaScriptSerializer.
        /// </summary>
        public int? MaxJsonLength
        {
            get;
            set;
        }

        /// <summary>
        /// When set RecursionLimit passed to the JavaScriptSerializer.
        /// </summary>
        public int? RecursionLimit
        {
            get;
            set;
        }

        #endregion

        #region ActionResult Overrides.

        /// <inheritdoc />
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (this.behavior.Equals(JsonRequestBehavior.DenyGet) &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            var model = context.Controller.ViewData.Model;
            if (model != null)
            {
                var serializer = new JavaScriptSerializer();
                if (MaxJsonLength.HasValue)
                {
                    serializer.MaxJsonLength = MaxJsonLength.Value;
                }
                if (RecursionLimit.HasValue)
                {
                    serializer.RecursionLimit = RecursionLimit.Value;
                }
                response.Write(serializer.Serialize(model));
            }
        }

        #endregion
    }
}