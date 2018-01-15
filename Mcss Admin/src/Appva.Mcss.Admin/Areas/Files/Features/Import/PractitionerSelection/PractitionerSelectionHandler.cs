// <copyright file="PractitionerSelectionHandler.cs" company="Appva AB">
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
    using Domain.VO;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerSelectionHandler : RequestHandler<Identity<PractitionerSelectionModel>, PractitionerSelectionModel>
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
        /// Initializes a new instance of the <see cref="PractitionerSelectionHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerSelectionHandler(IFileService fileService, ISettingsService settingsService)
        {
            this.fileService = fileService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerSelectionModel Handle(Identity<PractitionerSelectionModel> message)
        {
            var file       = this.fileService.Get(message.Id);
            var model      = new PractitionerSelectionModel();
            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if (file == null || 
                properties.PractitionerImportProperties == null || 
                properties.PractitionerImportProperties.IsImportable == false)
            {
                return model;
            }

            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var path     = this.fileService.SaveToDisk(file.Name, file.Data);
            var data     = ExcelReader.ReadPractitioners(
                path,
                settings.ImportPractitionerSettings.ValidateAtRow,
                settings.ImportPractitionerSettings.ValidColumns,
                out int lastRow,
                settings.ImportPractitionerSettings.ReadFromRow,
                null,
                settings.ImportPractitionerSettings.PreviewRows,
                true,
                true
            );
            File.Delete(path);

            model.Id            = file.Id;
            model.ValidateAtRow = settings.ImportPractitionerSettings.ValidateAtRow;
            model.ReadFromRow   = settings.ImportPractitionerSettings.ReadFromRow;
            model.Data          = data;
            model.LastRow       = lastRow;
            model.SkipRows      = settings.ImportPractitionerSettings.SkipRows;
            model.PreviewRows   = settings.ImportPractitionerSettings.PreviewRows;
            this.SetSelectedRowsTo(model, settings, properties);
            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Sets selected rows to the model.
        /// </summary>
        /// <param name="model">The <see cref="PractitionerSelectionModel"/>.</param>
        /// <param name="settings">The <see cref="FileConfiguration"/>.</param>
        /// <param name="properties">The <see cref="FileUploadProperties"/>.</param>
        private void SetSelectedRowsTo(PractitionerSelectionModel model, FileConfiguration settings, FileUploadProperties properties)
        {
            if (properties.PractitionerImportProperties.SelectedFirstRow.HasValue)
            {
                model.SelectedFirstRow = properties.PractitionerImportProperties.SelectedFirstRow.Value;
            }
            else
            {
                model.SelectedFirstRow = settings.ImportPractitionerSettings.ReadFromRow + 1;
            }

            if (properties.PractitionerImportProperties.SelectedLastRow.HasValue)
            {
                model.SelectedLastRow = properties.PractitionerImportProperties.SelectedLastRow.Value;
            }
            else
            {
                model.SelectedLastRow = model.LastRow + model.SkipRows + model.ValidateAtRow;
            }
        }

        #endregion
    }
}