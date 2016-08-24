// <copyright file="ListInventoryRecountItem.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoryRecountItem
    {
        #region Properties.

        /// <summary>
        /// The patient name.
        /// </summary>
        public string PatientName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient id.
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory id.
        /// </summary>
        public Guid InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory name.
        /// </summary>
        public string InventoryName
        {
            get;
            set;
        }

        #endregion
    }
}