// <copyright file="Person.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// An immutable changeset, or revision, containing modifications on 
    /// property level. 
    /// </summary>
    public class ChangeSet : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeSet"/> class.
        /// </summary>
        public ChangeSet() 
        {
            Changes = new List<Change>();
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The entity id.
        /// </summary>
        public virtual Guid EntityId
        {
            get;
            set;
        }

        /// <summary>
        /// The entity as string.
        /// </summary>
        public virtual string Entity
        {
            get;
            set;
        }

        /// <summary>
        /// The revision.
        /// </summary>
        public virtual int Revision
        {
            get;
            set;
        }

        /// <summary>
        /// Account
        /// </summary>
        public virtual Account ModifiedBy
        {
            get;
            set;
        }

        /// <summary>
        /// List of changes.
        /// </summary>
        public virtual IList<Change> Changes
        {
            get;
            set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Adds a change to the changeset.
        /// </summary>
        /// <param name="change"></param>
        public virtual void Add(Change change) 
        {
            Changes.Add(change);
        }

        #endregion
    }
}