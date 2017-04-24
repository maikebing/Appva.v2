// <copyright file="DeleteReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
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
    using Domain.Entities;
    using Domain.Repositories;
    using Persistence;
    #endregion

    /// <summary>
    /// TODO: Add a description to increase readability
    /// </summary>
    internal sealed class DeleteReasonHandler : RequestHandler<DeleteReasonModel, bool>
    {
        #region Fields
        /// <summary>
        /// The taxonomy service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The PersistanceContext
        /// </summary>
        private readonly IPersistenceContext persistenceContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteReasonHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The taxonomyservice</param>
        /// <param name="persistenceContext">The persistenceContext</param>
        public DeleteReasonHandler(ITaxonomyService taxonomyService, IPersistenceContext persistenceContext )
        {
            this.taxonomyService = taxonomyService;
            this.persistenceContext = persistenceContext;
        }
        #endregion

        #region RequestHandler Overrides
        /// <summary>
        /// Handles the RemoveReason request
        /// </summary>
        /// <param name="message">The model</param>
        /// <returns>true</returns>
        public override bool Handle(DeleteReasonModel message)
        {
            var taxon = this.taxonomyService.Find(message.Id, TaxonomicSchema.Withdrawal);

            if (taxon != null)
            {
                this.taxonomyService.Delete(taxon, TaxonomicSchema.Withdrawal);
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