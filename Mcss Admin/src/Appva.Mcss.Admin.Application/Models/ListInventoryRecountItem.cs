// <copyright file="ListInventoryRecountItems.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:your@email.address">Your name</a></author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListInventoryRecountItem
    {
        #region Properties.

        /// <summary>
        /// The patient name
        /// </summary>
        public string PatientName
        {
            get;
            set;
        }

        /// <summary>
        /// The patient id
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory id
        /// </summary>
        public Guid InventoryId
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory name
        /// </summary>
        public string InventoryName
        {
            get;
            set;
        }

        #endregion
    }
}