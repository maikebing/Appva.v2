// <copyright file="UploadPractitionerHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadPractitionerHandler : RequestHandler<Parameterless<UploadPractitionerModel>, UploadPractitionerModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadPractitionerHandler"/> class.
        /// </summary>
        public UploadPractitionerHandler()
        {
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override UploadPractitionerModel Handle(Parameterless<UploadPractitionerModel> message)
        {
            return new UploadPractitionerModel();
        }

        #endregion
    }
}