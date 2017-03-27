// <copyright file="EditGeneralSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{

    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    #endregion


    public class EditGeneralSettingsModel : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string MachineName { get; set; }

        public int? intValue { get; set; }

        public string stringValue { get; set; }

        public bool boolValue { get; set; }


        public string Name { get; set; }


    }
}