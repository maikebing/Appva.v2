// <copyright file="OperationalEnvironment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Environment
{
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
        Development,

        /// <summary>
        /// Unknown environment.
        /// </summary>
        Unknown
    }

    /// <summary>
    /// Extension helpers for <see cref="OperationalEnvironment"/>.
    /// </summary>
    public static class OperationalEnvironmentExtensions
    {
        /// <summary>
        /// Returns whether or not the current environment is in production mode.
        /// </summary>
        /// <param name="environment">The current environment</param>
        /// <returns>True if in production mode; otherwise false</returns>
        public static bool IsProduction(this OperationalEnvironment environment)
        {
            return environment.Equals(OperationalEnvironment.Production);
        }

        /// <summary>
        /// Returns whether or not the current environment is in demo mode.
        /// </summary>
        /// <param name="environment">The current environment</param>
        /// <returns>True if in demo mode; otherwise false</returns>
        public static bool IsDemo(this OperationalEnvironment environment)
        {
            return environment.Equals(OperationalEnvironment.Demo);
        }

        /// <summary>
        /// Returns whether or not the current environment is in staging mode.
        /// </summary>
        /// <param name="environment">The current environment</param>
        /// <returns>True if in staging mode; otherwise false</returns>
        public static bool IsStaging(this OperationalEnvironment environment)
        {
            return environment.Equals(OperationalEnvironment.Staging);
        }

        /// <summary>
        /// Returns whether or not the current environment is in development mode.
        /// </summary>
        /// <param name="environment">The current environment</param>
        /// <returns>True if in development mode; otherwise false</returns>
        public static bool IsDevelopment(this OperationalEnvironment environment)
        {
            return environment.Equals(OperationalEnvironment.Development);
        }

        /// <summary>
        /// Returns the user friendly text for the enum value.
        /// </summary>
        /// <param name="environment">The enum</param>
        /// <returns>A user friendly text representation of the enum value</returns>
        public static string AsUserFriendlyText(this OperationalEnvironment environment)
        {
            switch (environment)
            {
                case OperationalEnvironment.Production:
                    return "Production";
                case OperationalEnvironment.Demo:
                    return "Demo";
                case OperationalEnvironment.Staging:
                    return "Staging / Test";
                case OperationalEnvironment.Development:
                    return "Localhost";
                default:
                    return "Unknown";
            }
        }
    }
}