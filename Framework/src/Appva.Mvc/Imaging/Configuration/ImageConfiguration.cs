// <copyright file="ImageConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Imaging
{
    #region Imports.

    using Core.Configuration;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ImageConfiguration : IConfigurableResource
    {
        #region Public Properties.

        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Quality
        {
            get;
            set;
        }

        #endregion
    }
}