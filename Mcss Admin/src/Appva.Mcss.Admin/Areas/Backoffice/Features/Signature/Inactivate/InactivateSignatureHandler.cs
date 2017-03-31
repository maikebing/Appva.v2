// <copyright file="InactivateSignatureHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    #endregion

    /// <summary>
    /// Inactivates a signature
    /// </summary>
    public class InactivateSignatureHandler : RequestHandler<InactivateSignatureModel, bool>
    {
        #region Fields
        /// <summary>
        /// The Taxonomy Service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateSignatureHandler"/> 
        /// </summary>
        /// <param name="taxonomyService">Initializes the taxonomy service</param>
        public InactivateSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// <param name="message"></param>
        /// <returns>true or false</returns>
        /// </summary>
        public override bool Handle(InactivateSignatureModel message)
        {
            var signature = this.taxonomyService.Find(message.Id, TaxonomicSchema.SignStatus);

            // Set to inactive.
            signature.Update(false);
            this.taxonomyService.Update(signature, TaxonomicSchema.SignStatus, true);

            return true;
        }
    }
}