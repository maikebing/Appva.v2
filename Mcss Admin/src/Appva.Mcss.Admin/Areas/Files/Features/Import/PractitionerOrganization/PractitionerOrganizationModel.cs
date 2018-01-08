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
        /// A collection of unique organization nodes from the excel file.
        /// </summary>
        public IList<string> Nodes
        {
            get;
            set;
        }

        /// <summary>
        /// The selected node names.
        /// </summary>
        public IList<string> SelectedNodeNames
        {
            get;
            set;
        }

        /// <summary>
        /// The selected node ids.
        /// </summary>
        public IList<Guid> SelectedNodeIds
        {
            get;
            set;
        }

        #endregion
    }
}