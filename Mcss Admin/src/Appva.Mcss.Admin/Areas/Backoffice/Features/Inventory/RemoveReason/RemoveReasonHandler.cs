using Appva.Cqrs;
// <copyright file="RemoveReasonHandler.cs" company="Appva AB">
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
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Domain.Repositories;
    using Persistence;
    using Domain.Entities;
    #endregion

    internal sealed class RemoveReasonHandler : RequestHandler<RemoveReasonModel, bool>
    {
        #region Fields
        /// <summary>
        /// The taxonomy service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        private readonly IPersistenceContext persistenceContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveReasonHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The taxonomyservice</param>
        public RemoveReasonHandler(ITaxonomyService taxonomyService, IPersistenceContext persistenceContext )
        {
            this.taxonomyService = taxonomyService;
            this.persistenceContext = persistenceContext;
            
        }
        #endregion

        #region RequestHandler Overrides
        /// <summary>
        /// Handles the RemoveReason request
        /// </summary>
        /// <param name="message"></param>
        /// <returns>true</returns>
        public override bool Handle(RemoveReasonModel message)
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