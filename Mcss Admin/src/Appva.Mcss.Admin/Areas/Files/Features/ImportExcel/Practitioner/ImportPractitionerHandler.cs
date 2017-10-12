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
    using System.Data.OleDb;
    using System.Web;
    using System.IO;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using NPOI.XSSF.UserModel;

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

            if(file == null || Path.GetExtension(file.Name) != ".xlsx")
            {
                return model;
            }

            var path = this.SaveExcelFile(file.Name, file.Data);
            model.Data = this.ExcelToDataTable(path);
            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Reads data from an excel file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>A <see cref="DataTable"/>.</returns>
        private DataTable ExcelToDataTable(string path)
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

            int i = 0;
            while(sheet.GetRow(i) != null)
            {
                if (dataTable.Columns.Count < sheet.GetRow(i).Cells.Count)
                {
                    for (int j = 0; j < sheet.GetRow(i).Cells.Count; j++)
                    {
                        dataTable.Columns.Add();
                    }
                }

                dataTable.Rows.Add();

                for (int j = 0; j < sheet.GetRow(i).Cells.Count; j++)
                {
                    if (sheet.GetRow(i).GetCell(j) != null)
                    {
                        dataTable.Rows[i][j] = sheet.GetRow(i).GetCell(j);
                    }
                }

                i++;
            }

            File.Delete(path);

            return dataTable;
        }

        /// <summary>
        /// Saves the excel file to a folder.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="fileData">The file data.</param>
        /// <returns>The file path.</returns>
        private string SaveExcelFile(string fileName, byte[] fileData)
        {
            var folder = HttpContext.Current.Server.MapPath("~/Content/Temp");
            var path = string.Format("{0}\\{1}", folder, fileName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllBytes(path, fileData);

            return path;
        }

        #endregion
    }
}