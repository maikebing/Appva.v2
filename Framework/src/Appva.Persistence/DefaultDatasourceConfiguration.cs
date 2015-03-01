// <copyright file="DefaultDatasourceConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Persistence
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Default single database source configuration implementation of 
    /// <see cref="IDefaultDatasourceConfiguration"/>.
    /// </summary>
    public sealed class DefaultDatasourceConfiguration : IDefaultDatasourceConfiguration
    {
        #region IDefaultDatasourceConfiguration Members.

        /// <inheritdoc />
        public string ConnectionString
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Assembly
        {
            get;
            set;
        }

        /// <inheritdoc />
        public IDictionary<string, string> Properties
        {
            get;
            set;
        }

        #endregion
    }
}