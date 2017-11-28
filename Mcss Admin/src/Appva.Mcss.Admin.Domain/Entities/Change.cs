// <copyright file="Account.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Represents a change event for an instance.
    /// </summary>
    public class Change : Entity 
    {
        #region Properties.

        /// <summary>
        /// The property name.
        /// </summary>
        public virtual string Property
        {
            get;
            set;
        }

        /// <summary>
        /// the property type.
        /// </summary>
        public virtual string TypeOf
        {
            get;
            set;
        }

        /// <summary>
        /// The old state/value.
        /// </summary>
        public virtual string OldState
        {
            get;
            set;
        }

        /// <summary>
        /// The new state/value.
        /// </summary>
        public virtual string NewState
        {
            get;
            set;
        }

        /// <summary>
        /// Residing in the current changeset.
        /// </summary>
        public virtual ChangeSet ChangeSet
        {
            get;
            set;
        }

        #endregion
    }
}
