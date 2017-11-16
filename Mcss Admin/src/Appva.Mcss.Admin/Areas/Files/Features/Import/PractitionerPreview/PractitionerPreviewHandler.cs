// <copyright file="PractitionerPreviewHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerPreviewHandler : RequestHandler<Identity<PractitionerPreviewModel>, PractitionerPreviewModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerPreviewHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public PractitionerPreviewHandler(IFileService fileService, ISettingsService settingsService)
        {
            this.fileService = fileService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerPreviewModel Handle(Identity<PractitionerPreviewModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var model = new PractitionerPreviewModel();

            if (file == null || Path.GetExtension(file.Name) != ".xlsx")
            {
                return model;
            }

            var size = this.fileService.GetFileSizeFormat(file.Data.Length);
            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            int lastRow;
            var data = ExcelReader.ReadPractitioners(
                path, 
                settings.ImportPractitionerSettings.ValidateAtRow, 
                settings.ImportPractitionerSettings.ValidColumns, 
                settings.ImportPractitionerSettings.ReadFromRow, 
                out lastRow,
                settings.ImportPractitionerSettings.PreviewRows,
                true
            );
            File.Delete(path);

            model.FileId = message.Id;
            model.Title = file.Title;
            model.Description = file.Description;
            model.Name = file.Name;
            model.Size = size;
            model.Data = data;
            model.ValidateAtRow = settings.ImportPractitionerSettings.ValidateAtRow;
            model.ReadFromRow = settings.ImportPractitionerSettings.ReadFromRow;

            return model;
        }

        #endregion
    }
}