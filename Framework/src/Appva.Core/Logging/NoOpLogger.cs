// <copyright file="NoOpLogger.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Logging
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A no operation logger.
    /// </summary>
    internal sealed class NoOpLogger : ILog
    {
        #region ILog Members.

        /// <inheritdoc />
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception)
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsTraceEnabled()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsDebugEnabled()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsInfoEnabled()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsWarnEnabled()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsErrorEnabled()
        {
            return false;
        }

        /// <inheritdoc />
        public bool IsFatalEnabled()
        {
            return false;
        }

        #endregion
    }
}