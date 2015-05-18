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
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class RefillModel : ValueObject<RefillModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RefillModel"/> class.
        /// </summary>
        public RefillModel()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// If the sequence needs to be refilled
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

        public override bool Equals(RefillModel other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}