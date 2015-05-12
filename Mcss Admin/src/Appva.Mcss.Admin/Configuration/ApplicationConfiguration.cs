// <copyright file="ApplicationConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ApplicationConfiguration : ConfigurationSection
    {
        #region Variables.

        /// <summary>
        /// The environment configuration property.
        /// </summary>
        private const string EnvironmentConfigProperty = "environment";

        #endregion

        #region Properties.

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

        #region Private Classes.

        /// <summary>
        /// Converts a string representation of <see cref="OperationalEnvironment"/> to enum.
        /// </summary>
        private class OperationalEnvironmentConverter : ConfigurationConverterBase
        {
            /// <inheritdoc />
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object data) {
                return Enum.Parse(typeof(OperationalEnvironment), (string) data, true);
            }
        }

        #endregion
    }

    /// <summary>
    /// Describes the setting for where the application is put into operation for its 
    /// intended purpose.
    /// </summary>
    public enum OperationalEnvironment
    {
        /// <summary>
        /// Production environment.
        /// </summary>
        Production,

        /// <summary>
        /// Demo environment.
        /// </summary>
        Demo,

        /// <summary>
        /// Staging environment.
        /// </summary>
        Staging,

        /// <summary>
        /// Development environment.
        /// </summary>
        Development
    }

    /// <summary>
    /// Extension helpers for <see cref="OperationalEnvironment"/>.
    /// </summary>
    public static class OperationalEnvironmentExtensions
    {
        /// <summary>
        /// Returns the user friendly text for the enum value.
        /// </summary>
        /// <param name="environment">The enum</param>
        /// <returns>A user friendly text representation of the enum value</returns>
        public static string AsUserFriendlyText(this OperationalEnvironment environment)
        {
            switch (environment)
            {
                case OperationalEnvironment.Production :
                    return "Production";
                case OperationalEnvironment.Demo :
                    return "Demo";
                case OperationalEnvironment.Staging:
                    return "Staging / Test";
                case OperationalEnvironment.Development :
                    return "Localhost";
                default:
                    return "Unknown";
            }
        }
    }
}