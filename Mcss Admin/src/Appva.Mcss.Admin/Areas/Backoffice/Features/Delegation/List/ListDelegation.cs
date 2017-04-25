// <copyright file="ListDelegation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListDelegation
    {
        #region Properties.

        /// <summary>
        /// The total amount of delegations.
        /// </summary>
        public int TotalDelegationsCount
        {
            get;
            set;
        }

        /// <summary>
        /// The number of active delegations.
        /// </summary>
        public int ActiveDelegationsCount
        {
            get;
            set;
        }

        #endregion
    }
}