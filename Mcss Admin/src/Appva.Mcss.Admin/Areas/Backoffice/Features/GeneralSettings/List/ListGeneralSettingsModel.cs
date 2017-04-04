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
    using Cqrs;
    using JsonObjects.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    public class ListGeneralSettingsModel : IRequest<bool>
    {
        public List<ListGeneralSettings> List
        {
            get;
            set;
        }

        public PdfGenColors Colors { get; set; }

        public string BackgroundColor { get; set; }
    }
}