// <copyright file="GlobalResourceProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Compilation;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class GlobalResourceProvider : IResourceProvider
    {
        private IResourceProvider provider;

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResourceProvider"/> class.
        /// </summary>
        public GlobalResourceProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResourceProvider"/> class.
        /// </summary>
        public GlobalResourceProvider(IResourceProvider provider)
        {
            this.provider = provider;
        }

        #endregion

        public object GetObject(string resourceKey, System.Globalization.CultureInfo culture)
        {
            return this.provider.GetObject(resourceKey, System.Globalization.CultureInfo.CurrentCulture);
        }

        public System.Resources.IResourceReader ResourceReader
        {
            get { throw new NotImplementedException(); }
        }
    }
}