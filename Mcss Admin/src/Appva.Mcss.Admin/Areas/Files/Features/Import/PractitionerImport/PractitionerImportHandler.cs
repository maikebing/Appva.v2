// <copyright file="PractitionerImportHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerImportHandler : RequestHandler<PractitionerImportModel, PractitionerImportModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerImportHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerImportHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerImportModel Handle(PractitionerImportModel message)
        {
            var model = new PractitionerImportModel();
            var file = this.fileService.Get(message.FileId);

            if(file == null)
            {
                return model;
            }

            model.InvalidRowsCount = message.InvalidRowsCount;
            model.ImportedRowsCount = message.ImportedRowsCount;
            model.FileName = message.FileName;
            model.FileId = message.FileId;
            return model;
        }

        #endregion
    }
}