// <copyright file="ImportPractitionerHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.IO;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ImportPractitionerHandler : RequestHandler<Identity<ImportPractitionerModel>, ImportPractitionerModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportPractitionerHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public ImportPractitionerHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ImportPractitionerModel Handle(Identity<ImportPractitionerModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var model = new ImportPractitionerModel();

            if (file == null || Path.GetExtension(file.Name) != ".xlsx")
            {
                return model;
            }

            var size = this.fileService.GetFileSizeFormat(file.Data.Length);
            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            var data = ExcelReader.ReadPractitionersFromExcel(path, model.ValidateAtRow, model.ValidColumns, model.ReadFromRow, true);
            File.Delete(path);

            model.FileId = message.Id;
            model.Title = file.Title;
            model.Description = file.Description;
            model.Name = file.Name;
            model.Size = size;
            model.Data = data;

            return model;
        }

        #endregion
    }
}