// <copyright file="PermissionMeta.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public enum Visibility : int
    {
        /// <summary>
        /// Visibility is hidden.
        /// </summary>
        Hidden = 0,

        /// <summary>
        /// Visibility is visible.
        /// </summary>
        Visible = 1
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class NewAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NewAttribute"/> class.
        /// </summary>
        public NewAttribute()
        {
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class KeyAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyAttribute"/> class.
        /// </summary>
        /// <param name="value">The name value</param>
        public KeyAttribute(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The key value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class NameAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NameAttribute"/> class.
        /// </summary>
        /// <param name="value">The name value</param>
        public NameAttribute(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DescriptionAttribute"/> class.
        /// </summary>
        /// <param name="value">The description value</param>
        public DescriptionAttribute(string value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The description value.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class SortAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SortAttribute"/> class.
        /// </summary>
        /// <param name="value">The sort value</param>
        public SortAttribute(int value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The sort value.
        /// </summary>
        public int Value
        {
            get;
            private set;
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class VisibilityAttribute : Attribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="VisibilityAttribute"/> class.
        /// </summary>
        /// <param name="value">The visibility</param>
        public VisibilityAttribute(Visibility value)
        {
            this.Value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The visibility.
        /// </summary>
        public Visibility Value
        {
            get;
            private set;
        }

        #endregion
    }
}