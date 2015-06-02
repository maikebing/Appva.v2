// <copyright file="ExcelWriter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Office
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="path"></param>
        /// <param name="mapping"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static byte[] CreateNew<T, TResult>([NotNull] string path, [NotNull] Expression<Func<T, object>> mapping, [NotNull] IList<T> items)
        {
            AutoMap<T>.Map(mapping);
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
                        var result = AutoMap<T>.To<TResult>(item);
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
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
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

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class AutoMap<T>
    {
        /// <summary>
        /// The compiled cached expressions.
        /// </summary>
        private readonly static IDictionary<Type, Func<T, object>> Expressions = new Dictionary<Type, Func<T, object>>();

        /// <summary>
        /// Add mappings to the cache by type.
        /// </summary>
        /// <param name="expression"></param>
        public static void Map(Expression<Func<T, object>> expression)
        {
            if (! Expressions.ContainsKey(typeof(T)))
            {
                Expressions.Add(typeof(T), expression.Compile());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TResult To<TResult>(T obj)
        {
            return (Expressions.ContainsKey(typeof(T))) ? (TResult) Expressions[typeof(T)](obj) : default(TResult);
        }

    }
}