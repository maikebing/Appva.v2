// <copyright file="PlaceHolderAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Adds a place holder attribute to HTML tags.
    /// </summary>
    [Obsolete("Use @Html.FormGroup().TextBox(placeholderLabel) instead.")]
    public sealed class PlaceHolderAttribute : Attribute, IMetadataAware
    {
        #region Variables.

        /// <summary>
        /// The place holder text value.
        /// </summary>
        private readonly string placeholder;

        #endregion

        #region Constructor.

        /// <summary>
        ///  Initializes a new instance of the <see cref="PlaceHolderAttribute"/> class.
        /// </summary>
        /// <param name="placeholder">The place holder text value</param>
        public PlaceHolderAttribute(string placeholder) 
        {
            this.placeholder = placeholder;
        }

        #endregion

        #region IMetadataAware Members.

        /// <inheritdoc />
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues["placeholder"] = this.placeholder;
        }

        #endregion
    }
}