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

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// Inactivates a signature.
    /// </summary>
    public class InactivateSignatureHandler : RequestHandler<InactivateSignatureModel, bool>
    {
        #region Fields

        /// <summary>
        /// The Taxonomy Service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private IPersistenceContext persistenceContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateSignatureHandler"/> 
        /// </summary>
        /// <param name="taxonomyService">Initializes the taxonomy service</param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public InactivateSignatureHandler(ITaxonomyService taxonomyService, IPersistenceContext persistenceContext)
        {
            this.taxonomyService = taxonomyService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandler Overrides

        /// <summary>
        /// TODO: Add a descriptive summary to increase readability.
        /// <param name="message"></param>
        /// <returns></returns>
        /// </summary>
        public override bool Handle(InactivateSignatureModel message)
        {
            int usedByListsCount = this.persistenceContext.QueryOver<ScheduleSettings>()
                    .JoinQueryOver<Taxon>(x => x.StatusTaxons)
                    .Where(x => x.Id == message.Id)
                    .RowCount();

            if (usedByListsCount > 0)
                return false;

            var signature = this.taxonomyService.Find(message.Id, TaxonomicSchema.SignStatus);
            signature.Update(false);

            this.taxonomyService.Update(signature, TaxonomicSchema.SignStatus, true);

            return true;
        }

        #endregion
    }
}