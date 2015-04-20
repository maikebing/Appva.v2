// <copyright file="DateTimeUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Commands
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class DateTimeUtils
    {
        public static List<SelectListItem> GetMonthSelectList(int selectedMonth = 0)
        {
            return new List<SelectListItem>() { 
                            new SelectListItem() { Text="Januari", Value="1", Selected=selectedMonth.Equals(1) },
                            new SelectListItem() { Text="Februari", Value="2", Selected=selectedMonth.Equals(2) },
                            new SelectListItem() { Text="Mars", Value="3", Selected=selectedMonth.Equals(3) },
                            new SelectListItem() { Text="April", Value="4", Selected=selectedMonth.Equals(4) },
                            new SelectListItem() { Text="Maj", Value="5", Selected=selectedMonth.Equals(5) },
                            new SelectListItem() { Text="Juni", Value="6", Selected=selectedMonth.Equals(6) },
                            new SelectListItem() { Text="Juli", Value="7", Selected=selectedMonth.Equals(7) },
                            new SelectListItem() { Text="Augusti", Value="8", Selected=selectedMonth.Equals(8) },
                            new SelectListItem() { Text="September", Value="9", Selected=selectedMonth.Equals(9) },
                            new SelectListItem() { Text="Oktober", Value="10", Selected=selectedMonth.Equals(10) },
                            new SelectListItem() { Text="November", Value="11", Selected=selectedMonth.Equals(11) },
                            new SelectListItem() { Text="December", Value="12", Selected=selectedMonth.Equals(12) }
                        };
        }

        public static List<SelectListItem> GetYearSelectList(int year, int selectedYear = 0)
        {
            var YearList = new List<SelectListItem>();
            for (var currentYear = DateTime.Now.Year; currentYear >= year; currentYear--)
            {
                YearList.Add(new SelectListItem()
                {
                    Text = currentYear.ToString(),
                    Value = currentYear.ToString(),
                    Selected = currentYear.Equals(selectedYear)
                });
            }
            return YearList;
        }
    }
}