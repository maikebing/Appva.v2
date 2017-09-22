// <copyright file="EditUnits.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditUnits : IRequest<EditUnitsModel>
    {
        #region Properties.

        /// <summary>
        /// The model id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The unit name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// A string of dosage values.
        /// </summary>
        public string Values
        {
            get;
            set;
        }

        #endregion
    }
}