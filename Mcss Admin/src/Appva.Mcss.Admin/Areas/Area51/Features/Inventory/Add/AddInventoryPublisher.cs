﻿// <copyright file="AddInventoryPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddInventoryPublisher : RequestHandler<AddInventoryModel, bool>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddInventoryPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/></param>
        public AddInventoryPublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        /// <inheritdoc />
        public override bool Handle(AddInventoryModel message)
        {
            var name    = message.Name;
            var amounts = JsonConvert.DeserializeObject<List<double>>(message.Amounts);
            this.persistence.Save<Setting> (
                Setting.CreateNew(
                    string.Format("MCSS.Core.Inventory.Units.{0}", name), 
                    "MCSS.Core.Inventory.Units", 
                    name, 
                    "Inventory unit", 
                    JsonConvert.SerializeObject(amounts), 
                    typeof(IList<double>)));
            return true;
        }
    }
}