


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

    internal class AddReasonPublisher : RequestHandler<AddReasonModel, bool>
    {
        #region Fields
        /// <summary>
        /// The taxonomy service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;
        #endregion

        /// <summary>
        /// Initialized an instance of <see cref="AddReasonModel"/> class.
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/></param>
        public AddReasonPublisher(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        /// <summary>
        /// Adds the new taxon to database
        /// </summary>
        /// <param name="message">The Model</param>
        /// <returns>true or false</returns>
        public override bool Handle(AddReasonModel message)
        {
            if (message.Name != null)
            {
                var taxon = new TaxonItem(Guid.Empty, message.Name, string.Empty, string.Empty, string.Empty, 0, null);

                this.taxonomyService.Save(taxon, TaxonomicSchema.Withdrawal);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}