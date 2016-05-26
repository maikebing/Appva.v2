// <copyright file="SignatureModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SignatureModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureModel"/> class.
        /// </summary>
        public SignatureModel(Account account, DateTime date, Taxon status)
        {
            this.IsActive = true;
            this.SignatureDate = date;
            this.SignedBy = account;
            this.SignatureStatus = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureModel"/> class.
        /// </summary>
        public SignatureModel()
        {
        }

        #endregion

        #region Properties.

        public bool IsActive
        {
            get;
            set;
        }

        public DateTime SignatureDate
        {
            get;
            set;
        }

        public Account SignedBy
        {
            get;
            set;
        }

        public Taxon SignatureStatus
        {
            get;
            set;
        }

        #endregion
    }
}