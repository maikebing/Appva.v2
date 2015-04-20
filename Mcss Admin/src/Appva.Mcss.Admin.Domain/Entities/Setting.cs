// <copyright file="Setting.cs" company="Appva AB">
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
    public class Setting : AggregateRoot<Setting>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="context">The context/namespace</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="value">The value</param>
        /// <param name="type">The type</param>
        public Setting(string key, string context, string name, string description, string value, Type type)
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.MachineName = key;
            this.Namespace = context;
            this.Name = name;
            this.Description = description;
            this.Value = value;
            this.Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Setting()
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="context">The context/namespace</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="value">The value</param>
        /// <param name="type">The type</param>
        public static Setting CreateNew(string key, string context, string name, string description, string value, Type type)
        {
            return new Setting(key, context, name, description, value, type);
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether or not the <see cref="Setting"/> is active.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The Name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The value or a JSON object.
        /// </summary>
        public virtual string Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// The internal system name.
        /// </summary>
        public virtual string MachineName
        {
            get;
            protected set;
        }

        /// <summary>
        /// The section namespace.
        /// </summary>
        public virtual string Namespace
        {
            get;
            protected set;
        }

        /// <summary>
        /// The type.
        /// </summary>
        public virtual Type Type
        {
            get;
            protected set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Activates the setting.
        /// </summary>
        public virtual Setting Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        /// Inactivates the setting.
        /// </summary>
        public virtual Setting Inactivate()
        {
            this.IsActive = false;
            return this;
        }

        /// <summary>
        /// Updates the <see cref="Setting"/> class.
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="context">The context/namespace</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="value">The value</param>
        public virtual void Update(string key, string context, string name, string description, string value)
        {
            this.UpdatedAt = DateTime.Now;
            this.MachineName = key;
            this.Namespace = context;
            this.Name = name;
            this.Description = description;
            this.Value = value;
        }

        #endregion
    }
}