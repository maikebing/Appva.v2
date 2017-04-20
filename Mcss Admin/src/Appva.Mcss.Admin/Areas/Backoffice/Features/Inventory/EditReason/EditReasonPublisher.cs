// <copyright file="EditReasonPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Fields
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    #endregion

    internal class EditReasonPublisher : RequestHandler<EditReasonModel, bool>
    {
        #region Fields
        /// <summary>
        /// The taxonomy service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialized an instance of <see cref="AddReasonModel"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public EditReasonPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }
        #endregion

        #region RequestHandler Overrides
        /// <summary>
        /// Adds the new taxon to database
        /// </summary>
        /// <param name="message">The Model</param>
        /// <returns>true or false</returns>
        public override bool Handle(EditReasonModel message)
        {
            if (message.Name != null)
            {
                var taxon = new TaxonItem(message.Id, message.Name, string.Empty, string.Empty, string.Empty, 0, null);

                this.taxonomyService.Update(taxon, TaxonomicSchema.Withdrawal);

                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}