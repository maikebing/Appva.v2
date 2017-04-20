// <copyright file="RemoveReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RemoveReasonHandler : RequestHandler<RemoveReasonModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The<see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveReasonHandler"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public RemoveReasonHandler(ITaxonomyService taxonomyService, IPersistenceContext persistenceContext )
        {
            this.taxonomyService = taxonomyService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <summary>
        /// Handles the RemoveReason request
        /// </summary>
        /// <param name="message">The <see cref="RemoveReasonModel"/></param>
        /// <returns>Bool</returns>
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