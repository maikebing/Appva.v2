// <copyright file="DeleteDuplicate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeleteDuplicate : IRequest<Parameterless<List<DuplicatedAccount>>>
    {
        #region Properties.

        /// <summary>
        /// The account to remove
        /// </summary>
        public Guid AccountToRemove
        {
            get;
            set;
        }

        /// <summary>
        /// The accoutn to keep
        /// </summary>
        public Guid AccountToKeep
        {
            get;
            set;
        }

        #endregion
    }
}