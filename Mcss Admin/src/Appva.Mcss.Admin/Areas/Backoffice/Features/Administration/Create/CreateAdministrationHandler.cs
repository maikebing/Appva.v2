// <copyright file="CreateAdministrationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// The create administration handler.
    /// </summary>
    internal sealed class CreateAdministrationHandler : RequestHandler<CreateAdministration, CreateAdministrationModel>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAdministrationHandler"/> class.
        /// </summary>
        public CreateAdministrationHandler()
        {
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override CreateAdministrationModel Handle(CreateAdministration message)
        {
            return new CreateAdministrationModel
            {
                UnitSelectList = AdministrationValueModel.Units.Select(x => new SelectListItem { Text = x.Name, Value = x.Code }).ToList()
            };
        }

        #endregion
    }
}