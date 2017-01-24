// <copyright file="CreateOrganizationHandler.cs" company="Appva AB">
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
    internal sealed class CreateOrganizationUnitHandler : RequestHandler<Identity<CreateOrganizationalUnitModel>, CreateOrganizationalUnitModel>
    {     
        #region RequestHandler overrides

        /// <inheritdoc />
        public override CreateOrganizationalUnitModel Handle(Identity<CreateOrganizationalUnitModel> message)
        {
 	        return new CreateOrganizationalUnitModel
             {
                ParentId = message.Id
            };
        }

        #endregion
    }
}