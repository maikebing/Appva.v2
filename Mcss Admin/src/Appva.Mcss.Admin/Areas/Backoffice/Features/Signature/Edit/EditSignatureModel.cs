// <copyright file="EditSignatureModel.cs" company="Appva AB">
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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Appva.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditSignatureModel : IRequest<Identity<DetailsScheduleModel>>
    {
        #region Properties.

        /// <summary>
        /// The schedulesettings id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The options
        /// </summary>
        public IList<Tickable> Options
        {
            get;
            set;
        }

        #endregion
    }
}