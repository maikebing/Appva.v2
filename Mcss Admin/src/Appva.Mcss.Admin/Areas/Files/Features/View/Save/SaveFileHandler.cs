// <copyright file="SaveFileHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class SaveFileHandler : RequestHandler<SaveFile, FileContentResult>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService service;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileHandler"/> class.
        /// </summary>
        public SaveFileHandler(IFileService service)
        {
            this.service = service;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override FileContentResult Handle(SaveFile message)
        {
            var file = this.service.Get(message.Id);

            if(file == null)
            {
                return null;
            }

            return new FileContentResult(file.Data, file.ContentType)
            {
                FileDownloadName = file.Name
            };
        }

        #endregion
    }
}