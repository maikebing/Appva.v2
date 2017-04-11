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
    using Cqrs;
    using Domain.VO;
    using Ldap.Configuration;

    #endregion

    public class ListGeneralSettings
    {
        public Guid Id
        {
            get;
            set;
        }

        public int Index
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

        public string SubCategory
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public string CategoryColor
        {
            get;
            set;
        }

        public bool IsJson
        {
            get;
            set;
        }

        public PdfLookAndFeel PdfLookAndFeel
        {
            get;
            set;
        }

        public SecurityTokenConfiguration SecurityTokenConfig
        {
            get;
            set;
        }

        public SecurityMailerConfiguration SecurityMailerConfig
        {
            get;
            set;
        }
    }
}