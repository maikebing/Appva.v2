// <copyright file="DeletePreparation.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DeleteOrganizationalUnitHandler : RequestHandler<Identity<DeleteOrganizationalUnitModel>, DeleteOrganizationalUnitModel>
    {
        #region RequestHandler overrides

        public override DeleteOrganizationalUnitModel Handle(Identity<DeleteOrganizationalUnitModel> message)
        {
            return new DeleteOrganizationalUnitModel
            {
                Id = message.Id,
            };
        }

        #endregion
    }
}