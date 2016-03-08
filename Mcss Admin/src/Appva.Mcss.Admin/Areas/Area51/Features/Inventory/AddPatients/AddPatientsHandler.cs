// <copyright file="AddPatientsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Inventory.AddPatients
{
    #region Imports.

    using Appva.Cqrs;
using Appva.Mcss.Admin.Models;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class AddPatientsHandler : NotificationHandler<AddVersion162Notice>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private IPersistenceContext context;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPatientsHandler"/> class.
        /// </summary>
        public AddPatientsHandler(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        /// <inheritdoc />
        public override void Handle(AddVersion162Notice notification)
        {
            throw new NotImplementedException();
        }
    }
}