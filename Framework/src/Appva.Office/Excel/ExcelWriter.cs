// <copyright file="ExcelWriter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Office
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class ExcelWriter
    {
        /// <summary>
        /// Creates a new excel .xsl byte array.
        /// </summary>
        /// <typeparam name="T">The input type</typeparam>
        /// <typeparam name="TResult">The result type</typeparam>
        /// <param name="path">The template path</param>
        /// <param name="mapping">The expression mapping</param>
        /// <param name="items">The items to be mapped</param>
        /// <returns>An excel byte array</returns>
        public static byte[] CreateNew<T, TResult>([NotNull] string path, [NotNull] Expression<Func<T, object>> mapping, [NotNull] IList<T> items)
        {
            AutoMapper<T>.Map(mapping);
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var memory = new MemoryStream())
            {
                var workbook = new HSSFWorkbook(stream, true);
                var sheet = workbook.GetSheetAt(0);
                if (items.Count > 0)
                {
                    var rowIndex = 1;
                    foreach (var item in items)
                    {
                        var cellIndex = 0;
                        var row = sheet.CreateRow(rowIndex);
                        var result = AutoMapper<T>.To<TResult>(item);
                        var props = result.GetType().GetProperties();
                        foreach (var prop in props)
                        {
                            SetValue(row.CreateCell(cellIndex), prop.PropertyType, prop.GetValue(result, null));
                            cellIndex++;
                        }
                        rowIndex++;
                    }
                }
                workbook.Write(memory);
                return memory.ToArray();
            }
        }

        /// <summary>
        /// Sets a cell value.
        /// </summary>
        /// <param name="cell">The <see cref="ICell"/></param>
        /// <param name="type">The value type</param>
        /// <param name="value">The value</param>
        private static void SetValue([NotNull] ICell cell, [NotNull] Type type, object value)
        {
            if (value == null)
            {
                return;
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    cell.SetCellValue((string) value);
                    break;
                case TypeCode.DateTime:
                    cell.SetCellValue((DateTime) value);
                    break;
                case TypeCode.Boolean:
                    cell.SetCellValue((bool) value);
                    break;
                case TypeCode.Double:
                    cell.SetCellValue((double) value);
                    break;
                case TypeCode.Int32:
                    cell.SetCellValue((int) value);
                    break;
                default:
                    cell.SetCellValue(string.Empty);
                    break;
            }
        }
    }
}