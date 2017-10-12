// <copyright file="ImportPractitionerModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System.Data;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ImportPractitionerModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The excel data.
        /// </summary>
        public DataTable Data
        {
            get;
            set;
        }

        #endregion
    }
}