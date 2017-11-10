// <copyright file="PractitionerSelectionHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.Data;
    using System.IO;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;

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

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerSelectionHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerSelectionHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerSelectionModel Handle(Identity<PractitionerSelectionModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var model = new PractitionerSelectionModel();

            if (file == null || Path.GetExtension(file.Name) != ".xlsx")
            {
                return model;
            }

            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            //var data = ExcelReader.ReadPractitionersFromExcel(path, model.ValidateAtRow, model.ValidColumns, model.ReadFromRow, true);
            File.Delete(path);
            model.Data = new DataTable();

            return model;
        }

        #endregion
    }
}