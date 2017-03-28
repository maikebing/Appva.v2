// <copyright file="DelegationTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Transformers
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class DelegationTransformer
    {
        #region Transformations

        public static IList<DelegationViewModel> ToDelegationViewModel(IList<Delegation> delegations, IList<ITaxon> orgTaxons)
        {
            return delegations.Select(x => DelegationTransformer.ToDelegationViewModel(x, orgTaxons, true)).ToList();
        }


        public static DelegationViewModel ToDelegationViewModel(Delegation delegation, IList<ITaxon> orgTaxons, bool isEditableForCurrentPrincipal)
        {
            //// TODO: temp solution.
            var path = delegation.CreatedBy.Locations.OrderByDescending(x => x.Sort).First().Taxon.Path;
            return new DelegationViewModel
            {
                Id           = delegation.Id,
                Name         = delegation.Name,
                IsActivated  = !delegation.Pending,
                StartDate    = delegation.StartDate,
                EndDate      = delegation.EndDate,
                Patients     = delegation.Patients.ToDictionary(x => x.Id, x => x.FullName),
                Address      = orgTaxons.FirstOrDefault(x => x.Id == delegation.OrganisationTaxon.Id).Address,
                CreatedBy    = new Tuple<Guid, string, string>(delegation.CreatedBy.Id, delegation.CreatedBy.FullName, path),
                DelegationId = delegation.Taxon.Id,
                Category = DelegationTransformer.ToDelegationCategory(delegation.Taxon.Parent),
                IsEditableForCurrentPrincipal = isEditableForCurrentPrincipal
            };
        }

        /// <summary>
        /// Creates the delegation-category
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static DelegationCategoryModel ToDelegationCategory(Taxon delegation)
        {
            return new DelegationCategoryModel
            {
                Id   = delegation.Id,
                Name = delegation.Name
            };
        }

        #endregion

        
    }
}