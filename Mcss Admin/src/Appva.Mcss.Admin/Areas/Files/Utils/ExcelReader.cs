// <copyright file="ExcelReader.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Files.Excel
{
    #region Imports.

    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ExcelReader
    {
        #region Public static methods.

        /// <summary>
        /// Read practitioner data from an excel file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="validateAtRow">Validate columns at the specified row.</param>
        /// <param name="validColumns">The valid column data.</param>
        /// <param name="lastRow">The last row.</param>
        /// <param name="readFromRow">Read file from the specified row.</param>
        /// <param name="readToRow">Read file to the specified row.</param>
        /// <param name="previewRows">The number of rows that will be previewed.</param>
        /// <param name="isPreviewMode">If the reader will get a preview of the entire file.</param>
        /// <param name="skipInnerRows">Indicates if the preview will skip rows in the middle.</param>
        /// <returns>A <see cref="DataTable"/>.</returns>
        public static DataTable ReadPractitioners(
            string path, 
            int validateAtRow, 
            IList<string> validColumns,
            out int lastRow,
            int readFromRow,
            int? readToRow = null,
            int previewRows = 3,
            bool isPreviewMode = false, 
            bool skipInnerRows = false)
        {
            lastRow = 0;
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

            var dataHeader = dataTable.NewRow();
            for (int i = 0; i < sheet.GetRow(validateAtRow).LastCellNum; i++)
            {
                if (validColumns[i].ToLower() != sheet.GetRow(validateAtRow).GetCell(i).ToString().ToLower().Trim())
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
                if (isPreviewMode && skipInnerRows == false && j == (readFromRow + previewRows))
                {
                    break;
                }

                if(isPreviewMode && skipInnerRows && j >= readFromRow + previewRows && j < sheet.LastRowNum - previewRows - 1)
                {
                    j++;
                    continue;
                }

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
                lastRow = j;

                if(readToRow.HasValue && j >= readToRow.Value)
                {
                    break;
                }

                j++;
            }

            lastRow -= validateAtRow;
            return dataTable;
        }

        #endregion
    }
}