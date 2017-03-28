// <copyright file="Person.cs" company="Appva AB">
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
    public abstract class Person : AggregateRoot
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        protected Person()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The first name.
        /// </summary>
        public virtual string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The last name.
        /// </summary>
        public virtual string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// The Personal Identity Number / National Identity Number.
        /// </summary>
        public virtual PersonalIdentityNumber PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The full name.
        /// </summary>
        public virtual string FullName
        {
            get;
            set;
        }

        #endregion
    }
}