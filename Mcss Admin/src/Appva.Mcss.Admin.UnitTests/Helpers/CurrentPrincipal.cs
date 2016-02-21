// <copyright file="CurrentPrincipal.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Helpers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class CurrentPrincipal
    {
        #region Variables.

        /// <summary>
        /// The current principal ID.
        /// </summary>
        public static readonly IList<Guid> Ids = new List<Guid>();

        #endregion
    }
}