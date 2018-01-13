// <copyright file="UploadFileHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Web.Configuration;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadFileHandler : RequestHandler<Parameterless<UploadFileModel>, UploadFileModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFileHandler"/> class.
        /// </summary>
        public UploadFileHandler()
        {

        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UploadFileModel Handle(Parameterless<UploadFileModel> message)
        {
            var configuration = WebConfigurationManager.OpenWebConfiguration("~");
            var httpRuntimeSection = configuration.GetSection("system.web/httpRuntime") as HttpRuntimeSection;

            return new UploadFileModel
            {
                MaxFileSize = Math.Round(httpRuntimeSection.MaxRequestLength / 1024.0, 2)
            };
        }

        #endregion
    }
}