// <copyright file="ImageAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging
{
    #region Imports.

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Core.Extensions;
    using Detection;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ImageAttribute : ValidationAttribute
    {
        #region Public Properties.

        /// <summary>
        /// Sets the maximum file size;
        /// </summary>
        public int MaximumFileSize
        {
            get;
            set;
        }

        #endregion

        #region ValidationAttribute Overrides.

        /// <inheritdoc />
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return ValidationResult.Success;
            }
            var maximumFileSize = this.MaximumFileSize > 0 ? this.MaximumFileSize : (10 * 1024 * 1024);
            if (file.ContentLength > maximumFileSize)
            {
                return new ValidationResult("This file is too big!");
            }
            IDetector detector = new AutoDetector();
            var mediaType = detector.Detect(file.InputStream);
            if (mediaType.IsNull())
            {
                return new ValidationResult("Not supported file!");
            }
            /*string ext = Path.GetExtension(file.FileName);
            if (String.IsNullOrEmpty(ext) ||
               !ext.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("This file is not a PDF!");
            }*/

            // Everything OK.
            return ValidationResult.Success;
        }

        #endregion
    }
}