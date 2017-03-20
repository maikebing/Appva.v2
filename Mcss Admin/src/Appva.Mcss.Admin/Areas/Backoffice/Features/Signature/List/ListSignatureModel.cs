// <copyright file="ListSignatureModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
    using System.Collections.Generic;

    #endregion

    public class ListSignatureModel
    {
        public IList<ITaxon> Options
        {
            get; set;
        }
    }
}