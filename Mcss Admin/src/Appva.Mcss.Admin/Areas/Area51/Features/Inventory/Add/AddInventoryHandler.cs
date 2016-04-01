// <copyright file="AddInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddInventoryHandler : RequestHandler<Parameterless<AddInventoryModel>, AddInventoryModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddInventoryHandler"/> class.
        /// </summary>
        public AddInventoryHandler()
        {
        }

        #endregion

        public override AddInventoryModel Handle(Parameterless<AddInventoryModel> message)
        {
            var amounts = new List<double>();
            for (double i = 0; i <= 100; i++)
            {
                amounts.Add(i);
            }

            return new AddInventoryModel
            {
                Amounts = JsonConvert.SerializeObject(amounts)
            };
        }
    }
}