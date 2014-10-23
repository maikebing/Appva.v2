// <copyright file="Scope.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Scope : Entity<Client>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Scope(string key, string name, string description)
        {
            this.Key = key;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scope"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Scope()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The unique scope key.
        /// E.g. "read"
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The scope policy name.
        /// E.g. "Read"
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The scope policy description.
        /// E.g. "A client assigned this scope can
        /// create somthing for the resource something".
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// A single scope can be connected to multiple <see cref="Client"/>.
        /// </summary>
        public virtual IList<Client> Clients
        {
            get;
            protected set;
        }

        #endregion
    }
}