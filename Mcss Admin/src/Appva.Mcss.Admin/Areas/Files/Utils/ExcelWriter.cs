// <copyright file="ExcelWriter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Files.Excel
{
    #region Imports.

    using System.Collections.Generic;
    using System.IO;
    using NPOI.XSSF.UserModel;

    #endregion

    /// <summary>
    /// Modified excel writer from Appva.Office.
    /// </summary>
    public static class ExcelWriter
    {
        /// <summary>
        /// Creates a new excel byte array.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="path">The template path.</param>
        /// <param name="items">The items to be mapped.</param>
        /// <param name="writeFromRow">Writes data from the specified row number.</param>
        /// <returns>An excel byte array.</returns>
        public static byte[] Write<T>(string path, IList<T> items, int writeFromRow)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var memory = new MemoryStream())
            {
                var workbook = new XSSFWorkbook(stream);
                var sheet = workbook.GetSheetAt(0);

                if (items.Count > 0)
                {
                    var rowIndex = writeFromRow;
                    foreach (var item in items)
                    {
                        var cellIndex = 0;
                        var row = sheet.CreateRow(rowIndex);
                        var result = item;
                        var properties = result.GetType().GetProperties();

                        foreach (var property in properties)
                        {
                            row.CreateCell(cellIndex).SetCellValue((string)property.GetValue(result));
                            cellIndex++;
                        }
                        rowIndex++;
                    }
                }

                workbook.Write(memory);
                return memory.ToArray();
            }
        }
    }
}