// <copyright file="WebApiApplicationConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.WebApi.Configuration
{
    #region Imports.

    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using Appva.Core.Environment;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class WebApiApplicationConfiguration : ConfigurationSection
    {
        #region Variables.

        /// <summary>
        /// The environment configuration property.
        /// </summary>
        private const string EnvironmentConfigProperty = "environment";

        #endregion

        #region Properties.

        /// <summary>
        /// Gets the operational environment.
        /// </summary>
        [ConfigurationProperty(EnvironmentConfigProperty, DefaultValue = OperationalEnvironment.Development, IsRequired = true)]
        [TypeConverter(typeof(OperationalEnvironmentConverter))]
        public OperationalEnvironment Environment
        {
            get
            {
                return (OperationalEnvironment) this[EnvironmentConfigProperty];
            }

            private set
            {
                this[EnvironmentConfigProperty] = value;
            }
        }

        #endregion

        #region Constructor.

        /// <summary>
        /// Builds the <c>mcss</c> configuration section.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>A new <see cref="IApplicationEnvironment" /> instance</returns>
        public static IApplicationEnvironment For<T>()
        {
            var config = ConfigurationManager.GetSection("mcss") as WebApiApplicationConfiguration;
            return ApplicationEnvironment.For<T>(config.Environment);
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// Converts a string representation of <see cref="OperationalEnvironment"/> to enum.
        /// </summary>
        private class OperationalEnvironmentConverter : ConfigurationConverterBase
        {
            /// <inheritdoc />
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object data)
            {
                return Enum.Parse(typeof(OperationalEnvironment), (string)data, true);
            }
        }

        #endregion
    }
}