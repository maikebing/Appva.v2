// <copyright file = "ListGeneralSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Mcss.Admin.Areas.Backoffice.JsonObjects;
    using Appva.Mcss.Admin.Areas.Backoffice.JsonObjects.Pdf;

    #endregion

    public class ListGeneralSettings
    {
        public string ItemId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string MachineName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public string CategoryColorCode
        {
            get;
            set;
        }

        public string[] ItemColorCodes
        {
            get;
            set;
        }

        public int? IntValue
        {
            get;
            set;
        }

        public string StringValue
        {
            get;
            set;
        }

        public bool? BoolValue
        {
            get;
            set;
        }

        public bool IsJson
        {
            get;
            set;
        }

        public IList<double> ListValues
        {
            get;
            set;
        }

        public List<InventoryObject> InventoryObject
        {
            get;
            set;
        }

        public PdfGenObject PdfGenObject
        {
            get;
            set;
        }
    }
}