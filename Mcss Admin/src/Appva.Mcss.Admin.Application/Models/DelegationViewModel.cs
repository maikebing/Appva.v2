// <copyright file="DelegationViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationViewModel 
    {
        #region Properties

        /// <summary>
        /// The id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The Name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The address for the delegation
        /// </summary>
        public string Address
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the patients the delegation is valid for
        /// </summary>
        public IDictionary<Guid,string> Patients
        {
            get;
            set;
        }

        /// <summary>
        /// Startdate for the delegation
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Enddate for the delegation
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// If the delgation is active
        /// </summary>
        public bool IsActivated
        {
            get;
            set;
        }

        /// <summary>
        /// CreatedBy
        /// </summary>
        public Tuple<Guid, string, string> CreatedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The id of the delegation-taxon
        /// </summary>
        public Guid DelegationId 
        { 
            get; 
            set;
        }

        /// <summary>
        /// The category this delegation belongs to
        /// </summary>
        public DelegationCategoryModel Category
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the delegation is editable by the current principal.
        /// </summary>
        public bool IsEditableForCurrentPrincipal
        {
            get;
            set;
        }

        #endregion
    }
}