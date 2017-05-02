// <copyright file="EditDeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDeviceModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The device guid.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The device name.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The taxon id of the connected organization node.
        /// </summary>
        public Guid TaxonId
        {
            get;
            set;
        }

        /// <summary>
        /// The escalation level id.
        /// </summary>
        public Guid EscalationLevelId
        {
            get;
            set;
        }

        /// <summary>
        /// Organization nodes.
        /// </summary>
        public IEnumerable<SelectListItem> Organizations
        {
            get;
            set;
        }

        /// <summary>
        /// The devicealert
        /// </summary>
        public Guid CurrentEscalationLevelId
        {
            get;
            set;
        }

        /// <summary>
        /// The escalationlevels
        /// </summary>
        public IEnumerable<EscalationLevel> EscalationLevels
        {
            get;
            set;
        }


        #endregion
    }
}