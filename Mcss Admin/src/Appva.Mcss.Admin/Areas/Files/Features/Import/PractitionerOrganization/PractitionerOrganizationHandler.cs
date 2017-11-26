// <copyright file="PractitionerOrganizationHandler.cs" company="Appva AB">
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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerOrganizationHandler : RequestHandler<Identity<PractitionerOrganizationModel>, PractitionerOrganizationModel>
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
        /// Initializes a new instance of the <see cref="PractitionerOrganizationHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerOrganizationHandler(IFileService fileService, ISettingsService settingsService)
        {
            this.fileService = fileService;
            this.settingsService = settingsService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerOrganizationModel Handle(Identity<PractitionerOrganizationModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var model = new PractitionerOrganizationModel();
            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if (file == null ||
                properties.PractitionerImportProperties == null ||
                properties.PractitionerImportProperties.IsImportable == false)
            {
                return model;
            }

            int lastRow;
            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            var data = ExcelReader.ReadPractitioners(
                path,
                settings.ImportPractitionerSettings.ValidateAtRow,
                settings.ImportPractitionerSettings.ValidColumns,
                out lastRow,
                properties.PractitionerImportProperties.SelectedFirstRow.Value,
                properties.PractitionerImportProperties.SelectedLastRow
            );
            File.Delete(path);

            model.FileId = file.Id;
            model.UniqueNodes = this.GetNodes(
                data, 
                settings.ImportPractitionerSettings.ValidColumns.IndexOf(
                    settings.ImportPractitionerSettings.ValidColumns[5]
                )
            );

            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Gets a list of unique organization nodes.
        /// </summary>
        /// <param name="data">The excel <see cref="DataTable"/>.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>A collection of unique organization nodes.</returns>
        private IEnumerable<string> GetNodes(DataTable data, int columnIndex)
        {
            var nodes = new List<string>();

            for (var i = 1; i < data.Rows.Count; i++)
            {
                if(nodes.Contains(data.Rows[i][data.Columns[columnIndex]].ToString()) == false)
                {
                    nodes.Add(data.Rows[i][data.Columns[columnIndex]].ToString());
                }
            }

            return nodes;
        }

        #endregion
    }
}