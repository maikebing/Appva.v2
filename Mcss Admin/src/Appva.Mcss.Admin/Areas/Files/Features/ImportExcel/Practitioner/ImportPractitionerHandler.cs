// <copyright file="ImportPractitionerHandler.cs" company="Appva AB">
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
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using NPOI.XSSF.UserModel;
    using NPOI.SS.UserModel;

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

        /// <summary>
        /// The validation data.
        /// </summary>
        private readonly string[] validRowData =
        {
            "Personnummer",
            "Förnamn",
            "Efternamn",
            "E-post",
            "Roll",
            "Organisationstillhörighet",
            "HSA-id"
        };

        /// <summary>
        /// Validate cells at the specified row.
        /// </summary>
        private const byte validateAtRow = 2;

        /// <summary>
        /// Reads data from the specified row.
        /// </summary>
        private const byte readFromRow = 4;

        /// <summary>
        /// The number of rows in the excel file.
        /// </summary>
        private int rowCount;

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

            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            model.FileId = message.Id;
            model.Title = file.Title;
            model.Description = file.Description;
            model.Name = file.Name;
            model.Size = this.fileService.GetFileSizeFormat(file.Data.Length);
            model.ValidateAtRow = validateAtRow + 1;
            model.ReadFromRow = readFromRow;
            model.Data = this.ReadPractitionersFromExcel(path);
            model.RowCount = this.rowCount;

            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Read practitioner data from an excel file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>A <see cref="DataTable"/>.</returns>
        private DataTable ReadPractitionersFromExcel(string path)
        {
            var dataTable = new DataTable();
            XSSFWorkbook workbook;
            XSSFSheet sheet;
            string sheetName;

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fileStream);
                sheetName = workbook.GetSheetName(0);
            }

            sheet = (XSSFSheet)workbook.GetSheet(sheetName);

            try
            {
                var dataHeader = dataTable.NewRow();
                for (int i = 0; i < sheet.GetRow(validateAtRow).LastCellNum; i++)
                {
                    if (validRowData[i].ToLower() != sheet.GetRow(validateAtRow).GetCell(i).ToString().ToLower().Trim())
                    {
                        return null;
                    }

                    dataTable.Columns.Add();
                    dataHeader[i] = sheet.GetRow(validateAtRow).GetCell(i);
                }

                dataTable.Rows.Add(dataHeader);
                int j = readFromRow;

                while (sheet.GetRow(j) != null)
                {
                    if (sheet.GetRow(j).Cells.All(x => x.CellType == CellType.Blank))
                    {
                        j++;
                        continue;
                    }

                    var dataRow = dataTable.NewRow();

                    for (int k = 0; k < sheet.GetRow(j).LastCellNum; k++)
                    {
                        if (sheet.GetRow(j).GetCell(k) != null)
                        {
                            dataRow[k] = sheet.GetRow(j).GetCell(k);
                        }
                    }

                    dataTable.Rows.Add(dataRow);
                    this.rowCount++;
                    j++;
                }
            }
            finally
            {
                File.Delete(path);
            }
            
            return dataTable;
        }

        #endregion
    }
}