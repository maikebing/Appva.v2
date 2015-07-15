// <copyright file="SithsConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    #endregion

    /// <summary>
    /// The SITHS configuration.
    /// </summary>
    public interface ISithsConfiguration : IAuthifyConfiguration
    {
    }

    /// <summary>
    /// Implementation of <see cref="ISithsConfiguration"/>.
    /// </summary>
    public sealed class SithsConfiguration : ConfigurationSection, ISithsConfiguration
    {
        #region ISithsConfiguration Members.

        /// <inheritdoc />
        [ConfigurationProperty("serverAddress", IsRequired = true)]
        public Uri ServerAddress
        {
            get
            {
                return (Uri) this["serverAddress"];
            }

            set
            {
                this["serverAddress"] = value;
            }
        }

        /// <inheritdoc />
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return this["key"] as string;
            }

            set
            {
                this["key"] = value;
            }
        }

        /// <inheritdoc />
        [ConfigurationProperty("secret", IsRequired = true)]
        public string Secret
        {
            get
            {
                return this["secret"] as string;
            }

            set
            {
                this["secret"] = value;
            }
        }

        /// <inheritdoc />
        [ConfigurationProperty("redirectPath", IsRequired = true)]
        public string RedirectPath
        {
            get
            {
                return this["redirectPath"] as string;
            }

            set
            {
                this["redirectPath"] = value;
            }
        }

        /// <inheritdoc />
        public string IdentityProvider
        {
            get
            {
                return "siths";
            }
        }

        /// <inheritdoc />
        [ConfigurationProperty("resellerId", IsRequired = true)]
        public string ResellerId
        {
            get
            {
                return this["resellerId"] as string;
            }

            set
            {
                this["resellerId"] = value;
            }
        }

        #endregion
    }
}