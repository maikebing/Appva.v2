// <copyright file="PractitionerOrganizationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PractitionerOrganizationModel : IRequest<Guid>
    {
        #region Properties.

        /// <summary>
        /// The file id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// A collection of unique nodes from the imported file.
        /// </summary>
        public IList<string> ImportedNodes
        {
            get;
            set;
        }

        /// <summary>
        /// The selected nodes.
        /// </summary>
        public IList<string> SelectedNodes
        {
            get;
            set;
        }

        /// <summary>
        /// The parsed nodes.
        /// </summary>
        public IList<KeyValuePair<Guid, string>> ParsedNodes
        {
            get;
            set;
        }

        /// <summary>
        /// The taxons.
        /// </summary>
        public IList<TaxonViewModel> Taxons
        {
            get;
            set;
        }

        #endregion
    }
}