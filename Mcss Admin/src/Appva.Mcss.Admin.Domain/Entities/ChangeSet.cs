using System;
using System.Collections.Generic;
using Appva.Common.Domain;

namespace Appva.Mcss.Admin.Domain.Entities
{
    
    /// <summary>
    /// An immutable changeset, or revision, containing modifications on property level. 
    /// </summary>
    public class ChangeSet : AggregateRoot<ChangeSet>
    {

        #region Constructor.

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ChangeSet() {
            Changes = new List<Change>();
        }

        #endregion

        #region Public Fields.

        /// <summary>
        /// The entity id.
        /// </summary>
        public virtual Guid EntityId { get; set; }
        
        /// <summary>
        /// The entity as string.
        /// </summary>
        public virtual string Entity { get; set; }
        
        /// <summary>
        /// The revision.
        /// </summary>
        public virtual int Revision { get; set; }

        /// <summary>
        /// Account
        /// </summary>
        public virtual Account ModifiedBy { get; set; }

        /// <summary>
        /// List of changes.
        /// </summary>
        public virtual IList<Change> Changes { get; set; }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Adds a change to the changeset.
        /// </summary>
        /// <param name="change"></param>
        public virtual void Add(Change change) {
            Changes.Add(change);
        }

        #endregion

    }

}
