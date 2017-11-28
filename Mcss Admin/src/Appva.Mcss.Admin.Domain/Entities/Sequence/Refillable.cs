// <copyright file="RefillModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// A "trait" and/or component representing a handling of refill and
    /// orders.
    /// </summary>
    public class Refillable : ValueObject<Refillable>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Refillable"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        internal Refillable()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// If the sequence needs to be refilled ---
        /// If a stocked item is low in stock and needs to be refilled.
        /// TODO: Change property name, IsRefillNeeded 
        /// </summary>
        public virtual bool Refill
        {
            get;
            set;
        }

        /// <summary>
        /// The account which ordered the last refill
        /// </summary>
        public virtual Account RefillOrderedBy
        {
            get;
            set;
        }

        /// <summary>
        /// The date last refill was ordered
        /// </summary>
        public virtual DateTime? RefillOrderedDate
        {
            get;
            set;
        }

        /// <summary>
        /// If the sequence needs to be refilld and material have been ordered from manufacturer
        /// </summary>
        public virtual bool Ordered
        {
            get;
            set;
        }

        /// <summary>
        /// The account which made the order from manufacturer
        /// </summary>
        public virtual DateTime? OrderedDate
        {
            get;
            set;
        }

        /// <summary>
        /// When the sequence was ordered from manufacturer
        /// </summary>
        public virtual Account OrderedBy
        {
            get;
            set;
        }

        #endregion

        /// <inheritdoc />
        public override bool Equals(Refillable other)
        {
            //// WTF: Why isn't this implemented?
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}