// <copyright file="CodeAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Represents an alternate value for a method, property or field.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    internal sealed class CodeAttribute : Attribute
    {
        #region Variables.

        /// <summary>
        /// The code value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeAttribute"/> class.
        /// </summary>
        /// <param name="value">The code value.</param>
        public CodeAttribute(string value)
        {
            this.value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the value.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        public bool IsNoValue
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the code is prefixed.
        /// </summary>
        public bool IsPrefixed
        {
            get;
            set;
        }

        /// <summary>
        /// A prefix separator.
        /// </summary>
        public string PrefixSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not there are multiple values.
        /// </summary>
        public bool IsMany
        {
            get;
            set;
        }

        /// <summary>
        /// The many separator.
        /// </summary>
        public string ManySeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the value is a boolean.
        /// </summary>
        public bool IsBool
        {
            get;
            set;
        }

        /// <summary>
        /// The bool format.
        /// </summary>
        public BoolFormat Format
        {
            get;
            set;
        }

        #endregion

        public string GetAttributeName(object suffix = null)
        {
            return this.IsPrefixed == false ?
                this.Value :
                this.Value + (this.PrefixSeparator ?? string.Empty) + (suffix ?? string.Empty);
        }
    }

    internal enum BoolFormat
    {
        [Code("{0:true;0;false}")]
        TrueFalse,

        [Code("{0:yes;0;no}")]
        YesNo,

        [Code("{0:on;0;off}")]
        OnOff
    }
}