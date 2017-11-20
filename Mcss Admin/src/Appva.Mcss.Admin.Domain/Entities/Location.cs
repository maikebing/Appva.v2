// <copyright file="Location.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Location : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="account">
        /// The practitioner.
        /// </param>
        /// <param name="taxon">
        /// The taxon concept.
        /// </param>
        /// <param name="sort">
        /// Optional sorting; defaults to 0.
        /// </param>
        public Location(Account account, Taxon taxon, int sort = 0)
        {
            this.Account   = account;
            this.Taxon     = taxon;
            this.Sort      = sort;
            this.CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <remarks>Required by Nhibernate.</remarks>
        protected Location()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The location ID.
        /// </summary>
        public virtual Guid Id
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The created time and date.
        /// </summary>
        public virtual DateTime CreatedAt
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The practitioner.
        /// </summary>
        public virtual Account Account
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The codeable concept.
        /// </summary>
        /// <remarks>This is a <c>Taxon</c> in our case.</remarks>
        public virtual Taxon Taxon
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The sort
        /// </summary>
        public virtual int Sort
        {
            get;
            internal protected set;
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="account">
        /// The practitioner.
        /// </param>
        /// <param name="taxon">
        /// The taxon concept.
        /// </param>
        /// <param name="sort">
        /// Optional sorting; defaults to 0.
        /// </param>
        /// <returns>A new <see cref="Location"/> instance.</returns>
        public static Location New(Account account, Taxon taxon, int sort = 0)
        {
            return new Location(account, taxon, sort);
        }

        #endregion
    }
}