// <copyright file="Person.cs" company="Appva AB">
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
    public abstract class Person<T> : AggregateRoot<T>
        where T : class
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /*protected Person(string firstName, string lastName, string personalIdentityNumber)
        {
        }*/

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

        public virtual string FullName
        {
            get;
            set;
        }

        #endregion
    }
}