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

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    public class ListGeneralSettings
    {
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

        public string ColorCode
        {
            get;
            set;
        }
    }
}