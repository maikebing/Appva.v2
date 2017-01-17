// <copyright file="DeleteDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeleteDelegationModel : IRequest<ListDelegation>
    {
        #region Properties.

        /// <summary>
        /// The delegation id
        /// </summary>
        public Guid DelegationId
        {
            get;
            set;
        }

        /// <summary>
        /// The reason for delete
        /// </summary>
        [DisplayName("Ange orsak")]
        public string Reason
        {
            get;
            set;
        }

        /// <summary>
        /// The reason for delete
        /// </summary>
        public string ReasonText
        {
            get;
            set;
        }

        /// <summary>
        /// Reasons for delete
        /// </summary>
        public IList<SelectListItem> Reasons
        {
            get;
            set;
        }

        #endregion
    }
}