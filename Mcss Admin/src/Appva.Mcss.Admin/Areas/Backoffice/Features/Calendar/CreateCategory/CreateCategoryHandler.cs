// <copyright file="CreateCategoryHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateCategoryHandler : RequestHandler<Parameterless<CreateCategoryModel>, CreateCategoryModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryHandler"/> class.
        /// </summary>
        public CreateCategoryHandler()
        {
            
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override CreateCategoryModel Handle(Parameterless<CreateCategoryModel> message)
        {
            return new CreateCategoryModel();
        }

        #endregion
    }
}