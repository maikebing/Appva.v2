// <copyright file="PreviewTemplateHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PreviewTemplateHandler : RequestHandler<PreviewTemplate, PreviewTemplateModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTemplatesHandler"/> class.
        /// </summary>
        public PreviewTemplateHandler()
        {
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override PreviewTemplateModel Handle(PreviewTemplate message)
        {
            return new PreviewTemplateModel
            {
                Template = string.Format("/Areas/Notification/Features/Notification/Partials/Templates/{0}.cshtml", message.Template)
            };
        }

        #endregion
    }
}