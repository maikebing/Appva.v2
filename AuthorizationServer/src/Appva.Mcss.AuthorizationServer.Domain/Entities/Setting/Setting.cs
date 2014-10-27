// <copyright file="Setting.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Application settings.
    /// </summary>
    public class Setting : Entity<Setting>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="key">The settings key</param>
        /// <param name="value">The settings value</param>
        /// <param name="type">The value type</param>
        public Setting(string key, object value)
        {
            this.Key = key;
            this.Value = JsonConvert.SerializeObject(value);
            this.Type = value.GetType();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Setting()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The key.
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The value.
        /// </summary>
        public virtual string Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// The .NET type.
        /// </summary>
        public virtual Type Type
        {
            get;
            protected set;
        }

        #endregion
    }
}