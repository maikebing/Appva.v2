// <copyright file="ImageConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging.Configuration
{
    #region Imports.

    using Core;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ImageConfiguration : IConfigurableResource
    {
        /// <summary>
        /// The file path.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// The resize width.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// The resize height.
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// The resize quility.
        /// </summary>
        public int Quality
        {
            get;
            set;
        }
    }
}