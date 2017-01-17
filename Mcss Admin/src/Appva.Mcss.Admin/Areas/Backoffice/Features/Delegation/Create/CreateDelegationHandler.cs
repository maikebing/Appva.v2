// <copyright file="CreateDelegationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Models;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateDelegationHandler : RequestHandler<Identity<CreateDelegationModel>, CreateDelegationModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationHandler"/> class.
        /// </summary>
        public CreateDelegationHandler()
        {
        }

        #endregion
    
        #region RequestHandler overrides.

        /// <inheritdoc />
        public override CreateDelegationModel Handle(Identity<CreateDelegationModel> message)
        {
 	        return new CreateDelegationModel
            {
                CategoryId = message.Id
            };
        }

        #endregion
    }
}