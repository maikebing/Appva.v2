﻿// <copyright file="UpdateInventoryModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateInventoryModel : IRequest<ListInventory>
    {
        #region Properties

        /// <summary>
        /// The id of the inventory
        /// </summary>
        public Guid Inventory
        {
            get;
            set;
        }

        /// <summary>
        /// The id of the patient
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The descriptive name of the invnetory
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory unit
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// The available withdrawal amounts
        /// </summary>
        public string Amounts
        {
            get;
            set;
        }

        #endregion
    }
}