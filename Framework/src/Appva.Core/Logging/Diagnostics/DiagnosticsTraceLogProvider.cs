// <copyright file="DiagnosticsTraceLogProvider.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Logging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    #endregion

    /// <summary>
    /// Implementation of <see cref="ILogProvider"/> that uses the <see cref="DiagnosticsTraceLogger"/>.
    /// </summary>
    public class DiagnosticsTraceLogProvider : ILogProvider
    {
        #region ILogProvider Members.

        /// <inheritdoc />
        public ILog GetLogger(string name)
        {
            return new DiagnosticsTraceLogger(name);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of <see cref="ILog"/> that uses <see cref="System.Diagnostics.Trace"/>.
    /// <example>
    /// <configuration>
    ///   <system.diagnostics>
    ///     <trace>
    ///       <listeners>
    ///         <add name=""
    ///           type="Microsoft.CodeAnalysis.TraceListener, Microsoft.CodeAnalysis, Version=..."
    ///           initializeData="true"/>
    ///         <remove name="Default"/>
    ///       </listeners>
    ///     </trace>
    ///   </system.diagnostics>
    /// </configuration>
    /// </example>
    /// </summary>
    public class DiagnosticsTraceLogger : ILog
    {
        #region Variables.

        /// <summary>
        /// The logger name.
        /// </summary>
        private readonly string name;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTraceLogger"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DiagnosticsTraceLogger(string name)
        {
            this.name = string.Format("[{0}]", name);
        }

        #endregion

        #region ILog Members.

        /// <summary>
        /// Logs the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="messageFunc">The message function.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            if (messageFunc != null)
            {
                if (exception == null)
                {
                    var message = string.Format("{0}: {1} -- {2}", name, DateTimeOffset.UtcNow, messageFunc());
                    TraceMsg(logLevel, message);
                }
                else
                {
                    var message = string.Format("{0}: {1} -- {2}\n{3}", name, DateTimeOffset.UtcNow, messageFunc(), exception);
                    TraceMsg(logLevel, message);
                }
            }

            return true;
        }

        /// <summary>
        /// Log a message and exception at the specified log level.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="logLevel">The log level.</param>
        /// <param name="messageFunc">The message function.</param>
        /// <param name="exception">The exception.</param>
        /// <remarks>
        /// Note to implementors: the message func should not be called if the loglevel is not enabled
        /// so as not to incur perfomance penalties.
        /// </remarks>
        public void Log<TException>(LogLevel logLevel, Func<string> messageFunc, TException exception) where TException : Exception
        {
            if (messageFunc != null && exception != null)
            {
                var message = string.Format("{0}: {1} -- {2}\n{3}", name, DateTimeOffset.UtcNow.ToString(), messageFunc(), exception);
                TraceMsg(logLevel, message);
            }
        }

        /// <inheritdoc />
        public bool IsTraceEnabled()
        {
            return true;
        }

        /// <inheritdoc />
        public bool IsDebugEnabled()
        {
            return true;
        }

        /// <inheritdoc />
        public bool IsInfoEnabled()
        {
            return true;
        }

        /// <inheritdoc />
        public bool IsWarnEnabled()
        {
            return true;
        }

        /// <inheritdoc />
        public bool IsErrorEnabled()
        {
            return true;
        }

        /// <inheritdoc />
        public bool IsFatalEnabled()
        {
            return true;
        }

        #endregion

        #region Provate Methods.

        private static void TraceMsg(LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    Trace.WriteLine(message, logLevel.ToString());
                    break;
                case LogLevel.Info:
                    Trace.TraceInformation(message);
                    break;
                case LogLevel.Warn:
                    Trace.TraceWarning(message);
                    break;
                case LogLevel.Error:
                    Trace.TraceError(message);
                    break;
                case LogLevel.Fatal:
                    Trace.TraceError(string.Format("FATAL : {0}", message));
                    break;
            }
        }

        #endregion
    }
}