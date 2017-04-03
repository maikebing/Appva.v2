// <copyright file="ListSignatureHandler.cs" company="Appva AB">
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
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Infrastructure.Models;

    #endregion

    internal sealed class ListSignatureHandler : RequestHandler<Parameterless<ListSignatureModel>, ListSignatureModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListSignatureHandler"/> class.
        /// </summary>
        public ListSignatureHandler(ITaxonomyService taxonomyService, IPersistenceContext persistenceContext)
        {
            this.taxonomyService = taxonomyService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandlers Overrides.

        /// <inheritdoc />
        public override ListSignatureModel Handle(Parameterless<ListSignatureModel> message)
        {
            var signingList = this.taxonomyService.List(TaxonomicSchema.SignStatus);
            var signingModel = new ListSignatureModel();
            signingModel.Items = new List<ListSignature>();

            foreach (var item in signingList)
            {
                var signing = new ListSignature();

                var itemInUseCount = this.persistenceContext.QueryOver<ScheduleSettings>()
                    .JoinQueryOver<Taxon>(x => x.StatusTaxons)
                    .Where(x => x.Id == item.Id)
                    .RowCount();

                signing.Id = item.Id;
                signing.Name = item.Name;
                signing.Path = item.Path;
                signing.IsUsedByList = itemInUseCount > 0 ? true : false;

                signingModel.Items.Add(signing);
            }

            return signingModel;
        }

        #endregion
    }
}