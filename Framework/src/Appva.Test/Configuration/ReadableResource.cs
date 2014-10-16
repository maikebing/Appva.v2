// <copyright file="ReadableResource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Configuration
{
    #region Import.

    using System;
    using System.Collections.Generic;
    using Appva.Core.Configuration;

    #endregion

    /// <summary>
    /// A <see cref="IConfigurableApplicationContext"/> for read/write test.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    internal sealed class ReadableResource : IConfigurableResource
    {
        /// <summary>
        /// Gets and sets the id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the value.
        /// </summary>
        public long Value
        {
            get;
            set;
        }
    }
}
