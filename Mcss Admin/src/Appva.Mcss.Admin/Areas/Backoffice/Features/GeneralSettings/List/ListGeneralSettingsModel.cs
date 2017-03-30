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

    public class ListGeneralSettingsModel
    {
        public IEnumerable<Setting> List { get; set; }
    }
}