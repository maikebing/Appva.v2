// <copyright file="CreateInventoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateInventoryHandler : RequestHandler<Identity<CreateInventoryModel>, CreateInventoryModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInventoryHandler"/> class.
        /// </summary>
        public CreateInventoryHandler()
        {
        }

        #endregion

        #region RequestHandler Overrides

        /// <inheritdoc />
        public override CreateInventoryModel Handle(Identity<CreateInventoryModel> message)
        {
            return new CreateInventoryModel
            {
                Id = message.Id
            };
        }

        #endregion
    }
}