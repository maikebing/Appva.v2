// <copyright file="Entity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Persistence
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Anemic Entity test.
    /// </summary>
    public class Entity
    {
        #region Properties.

        /// <summary>
        /// Returns the id.
        /// </summary>
        public virtual Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        #endregion
    }
}