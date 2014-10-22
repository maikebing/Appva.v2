// <copyright file="TaxonTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Web;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;

    #endregion

    /// <summary>
    /// Taxon transforming.
    /// </summary>
    public class TaxonTransformer
    {
        /// <summary>
        /// TODO: contentDirectory.
        /// </summary> 
        private static string contentDirectory = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).Replace("http", "https").Replace(":8181", string.Empty) + "/Content/i/"; 

        /// <summary>
        /// Transforms a <see cref="Taxon"/> to a <see cref="TaxonModel"/>
        /// </summary>
        /// <param name="taxon">TODO: taxon</param>
        /// <param name="isParent">TODO: isParent</param>
        /// <param name="patientCount">TODO: patientCount</param>
        /// <returns>TODO: returns</returns>
        public static TaxonModel ToTaxon(Taxon taxon, bool isParent, string filter, int patientCount = 0)
        {
            return new TaxonModel
            {
                Id = taxon.Id,
                Name = taxon.Name,
                Description = taxon.Description,
                Type = FromTaxonomyToType(taxon.Taxonomy),
                IsRoot = taxon.Id.ToString().Equals(filter) ? true : taxon.IsRoot,
                IncompleteTask = 0,
                PatientCount = patientCount,
                HasChildren = isParent
            };
        }

        /// <summary>
        /// Transforms a collection of <see cref="Taxon"/> to a collection of <see cref="TaxonModel"/>.
        /// </summary>
        /// <param name="taxons">TODO: taxons</param>
        /// <param name="parents">TODO: parents</param>
        /// <param name="patientCounts">TODO: patientCounts</param>
        /// <returns>TODO: returns</returns>
        public static IList<TaxonModel> ToTaxon(IList<Taxon> taxons, IList<Guid> parents, IDictionary<Guid, int> patientCounts, string filter)
        {
            var retval = new List<TaxonModel>();
            foreach (var taxon in taxons)
            {
                var pc = patientCounts.ContainsKey(taxon.Id) ? patientCounts[taxon.Id] : 0;
                retval.Add(ToTaxon(taxon, parents.Contains(taxon.Id), filter, pc));
            }
            return retval;
        }

        /// <summary>
        /// Transforms a <code>Taxon</code> to a patient-profile.
        /// </summary>
        /// <param name="taxon">TODO: taxons</param>
        /// <returns>TODO: returns</returns>
        public static ProfileModel ToProfile(Taxon taxon)
        {
            return new ProfileModel
            {
                Id = taxon.Id,
                Name = taxon.Name,
                Description = taxon.Description,
                Image = string.Format("{0}{1}", contentDirectory, taxon.Type)
            };
        }

        /// <summary>
        /// Transforms a collection of <code>Taxon</code> to a collection of patient-profiles.
        /// </summary>
        /// <param name="taxons">TODO: taxons</param>
        /// <returns>TODO: returns</returns>
        public static IList<ProfileModel> ToProfile(IList<Taxon> taxons)
        {
            var retval = new List<ProfileModel>();
            foreach (var taxon in taxons)
            {
                retval.Add(ToProfile(taxon));
            }
            return retval;
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="taxons">TODO: taxons</param>
        /// <param name="contactRef">TODO: contactRef</param>
        /// <returns>TODO: returns</returns>
        public static List<StatusItemModel> ToStatusItemModel(IList<Taxon> taxons, string contactRef = null)
        {
            var retval = new List<StatusItemModel>();
            foreach (var taxon in taxons)
            {
                retval.Add(ToStatusItemModel(taxon, contactRef));
            }
            return retval;
        }

        /// <summary>
        /// TODO: Summary.
        /// </summary>
        /// <param name="taxon">TODO: taxon</param>
        /// <param name="contactRef">TODO: contactRef</param>
        /// <returns>TODO: returns</returns>
        public static StatusItemModel ToStatusItemModel(Taxon taxon, string contactRef = null)
        {
            return new StatusItemModel
            { 
                Id = taxon.Id,
                Name = taxon.Name,
                ImageUrl = string.Format("{0}{1}", contentDirectory, taxon.Path),
                ContactRef = taxon.Weight != 1 ? contactRef : null
            };
        }

        #region Helpers

        /// <summary>
        /// Convert Taxonomy MachineName to Taxon type.
        /// </summary>
        /// <param name="taxonomy">TODO: taxonomy</param>
        /// <returns>TODO: returns</returns>
        public static string FromTaxonomyToType(Taxonomy taxonomy)
        {
            switch (taxonomy.MachineName)
            { 
                case "ORG":
                    return "organization";
                case "DEL":
                    return "delegation";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Convert collection of <code>Taxonomy</code> to collection of <code>Taxon</code> type 
        /// </summary>
        /// <param name="taxonomys">TODO: taxonomys</param>
        /// <returns>Collection of <code>string</code> (Taxon type)</returns>
        public static List<string> FromTaxonomyToType(IList<Taxonomy> taxonomys)
        {
            var retval = new List<string>();
            foreach (var taxonomy in taxonomys)
            {
                retval.Add(FromTaxonomyToType(taxonomy));
            }
            return retval;
        }

        /// <summary>
        /// Converts type to Taxonomy MachineName.
        /// </summary>
        /// <param name="type">TODO: type</param>
        /// <returns><code>Taxonomy</code> MachineName</returns>
        public static string FromTypeToTaxonomy(string type) 
        {
            switch (type)
            { 
                case "organization":
                    return "ORG";
                case "delegation":
                    return "DEL";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts a collection of Types to a collection of <code>Taxonomy</code> MachineName.
        /// </summary>
        /// <param name="types">TODO: types</param>
        /// <returns>Collection of <code>Taxonomy</code> MachineName</returns>
        public static List<string> FromTypeToTaxonomy(List<string> types)
        {
            var retval = new List<string>();
            foreach (var type in types)
            {
                retval.Add(FromTypeToTaxonomy(type));
            }
            return retval;
        }

        #endregion
    }
}