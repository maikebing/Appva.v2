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
    public sealed class GlobalResourceProviderFactory : ResourceProviderFactory
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResourceProvider"/> class.
        /// </summary>
        public GlobalResourceProviderFactory()
        {
        }

        #endregion

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            var factory = (ResourceProviderFactory)Activator.CreateInstance(Type.GetType("System.Web.Compilation.ResXResourceProviderFactory, System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
            var provider = factory.CreateGlobalResourceProvider(classKey);

            return new GlobalResourceProvider(provider);
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            throw new NotImplementedException();
        }
    }
}