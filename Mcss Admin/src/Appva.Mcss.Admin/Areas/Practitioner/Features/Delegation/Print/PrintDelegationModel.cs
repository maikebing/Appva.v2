// <copyright file="PrintDelegationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Models
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
    public sealed class PrintDelegationModel
    {
        #region Properties.

        /// <summary>
        /// The recipient account
        /// </summary>
        public Account DelegationRecipient 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The issuer account
        /// </summary>
        public Account DelegationIssuer 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// List of delegations
        /// </summary>
        public IList<DelegationViewModel> Delegations 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// List of knowledge tests
        /// </summary>
        public IList<KnowledgeTest> KnowledgeTests 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The text about who the delegationpaper should be snet to
        /// </summary>
        public string SendToText
        {
            get;
            set;
        }

        #endregion
    }
}