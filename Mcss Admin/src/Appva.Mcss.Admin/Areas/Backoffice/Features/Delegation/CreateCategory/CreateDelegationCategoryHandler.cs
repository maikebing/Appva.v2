// <copyright file="CreateDelegationCategoryHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateDelegationCategoryHandler : RequestHandler<Parameterless<CreateDelegationCategoryModel>, CreateDelegationCategoryModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDelegationCategoryHandler"/> class.
        /// </summary>
        public CreateDelegationCategoryHandler()
        {
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override CreateDelegationCategoryModel Handle(Parameterless<CreateDelegationCategoryModel> message)
        {
            return new CreateDelegationCategoryModel();
        }

        #endregion
    }
}