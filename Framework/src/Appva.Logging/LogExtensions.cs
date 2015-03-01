// <copyright file="LogExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="https://github.com/damianh/LibLog">Damian Hickey</a>
// </author>
// Copyright © 2011-2014 Damian Hickey. All rights reserved.
// Permission is hereby granted, free of charge, to any person obtaining a copy of 
// this software and associated documentation files (the "Software"), to deal in 
// the Software without restriction, including without limitation the rights to 
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
// the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all 
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
namespace Appva.Logging
{
    #region Imports.

    using System;
    using System.Globalization;

    #endregion

    /// <summary>
    /// <see cref="ILog"/> extensions.
    /// </summary>
    public static class LogExtensions
    {
        #region Public Static Methods.

        #region Trace Logging.

        /// <summary>
        /// Whether or not trace is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if trace is enabled</returns>
        public static bool IsTraceEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Trace, null);
        }

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Trace(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Trace, messageFunc);
        }

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Trace(this ILog logger, string message)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs a trace message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void TraceFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsTraceEnabled())
            {
                logger.LogFormat(LogLevel.Trace, message, args);
            }
        }

        /// <summary>
        /// Logs a trace message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void TraceException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, message.AsFunc(), exception);
            }
        }

        #endregion

        #region Debug Logging.

        /// <summary>
        /// Whether or not debug is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if debug is enabled</returns>
        public static bool IsDebugEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Debug, null);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Debug(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Debug, messageFunc);
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Debug(this ILog logger, string message)
        {
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs a debug message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void DebugFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsDebugEnabled())
            {
                logger.LogFormat(LogLevel.Debug, message, args);
            }
        }

        /// <summary>
        /// Logs a debug message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void DebugException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, message.AsFunc(), exception);
            }
        }

        #endregion

        #region Information Logging.

        /// <summary>
        /// Whether or not info is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if info is enabled</returns>
        public static bool IsInfoEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Info, null);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Info(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Info, messageFunc);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Info(this ILog logger, string message)
        {
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs an information message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void InfoFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsInfoEnabled())
            {
                logger.LogFormat(LogLevel.Info, message, args);
            }
        }

        /// <summary>
        /// Logs an information message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void InfoException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, message.AsFunc(), exception);
            }
        }

        #endregion

        #region Warning Logging.

        /// <summary>
        /// Whether or not warn is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if warn is enabled</returns>
        public static bool IsWarnEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Warn, null);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Warn(this ILog logger, Func<string> messageFunc)
        {
            GuardAgainstNullLogger(logger);
            logger.Log(LogLevel.Warn, messageFunc);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Warn(this ILog logger, string message)
        {
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs a warning message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void WarnFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsWarnEnabled())
            {
                logger.LogFormat(LogLevel.Warn, message, args);
            }
        }

        /// <summary>
        /// Logs a warning message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void WarnException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, message.AsFunc(), exception);
            }
        }

        #endregion

        #region Error Logging.

        /// <summary>
        /// Whether or not error is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if error is enabled</returns>
        public static bool IsErrorEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Error, null);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Error(this ILog logger, Func<string> messageFunc)
        {
            logger.Log(LogLevel.Error, messageFunc);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Error(this ILog logger, string message)
        {
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs an error message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void ErrorFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsErrorEnabled())
            {
                logger.LogFormat(LogLevel.Error, message, args);
            }
        }

        /// <summary>
        /// Logs an error message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void ErrorException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, message.AsFunc(), exception);
            }
        }

        #endregion

        #region Fatal Logging.

        /// <summary>
        /// Whether or not fatal is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <returns>True if fatal is enabled</returns>
        public static bool IsFatalEnabled(this ILog logger)
        {
            GuardAgainstNullLogger(logger);
            return logger.Log(LogLevel.Fatal, null);
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Fatal(this ILog logger, Func<string> messageFunc)
        {
            logger.Log(LogLevel.Fatal, messageFunc);
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Fatal(this ILog logger, string message)
        {
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, message.AsFunc());
            }
        }

        /// <summary>
        /// Logs a fatal message with custom formatting.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The message format</param>
        /// <param name="args">The object arguments</param>
        public static void FatalFormat(this ILog logger, string message, params object[] args)
        {
            if (logger.IsFatalEnabled())
            {
                logger.LogFormat(LogLevel.Fatal, message, args);
            }
        }

        /// <summary>
        /// Logs a fatal message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        /// <param name="exception">The exception</param>
        public static void FatalException(this ILog logger, string message, Exception exception)
        {
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, message.AsFunc(), exception);
            }
        }

        #endregion

        #endregion

        #region Private Static Methods.

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the <see cref="ILog"/>
        /// is null.
        /// </summary>
        /// <param name="logger">The <see cref="ILog"/></param>
        private static void GuardAgainstNullLogger(ILog logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }
        }

        /// <summary>
        /// Formats the log message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="logLevel">The <see cref="LogLevel"/></param>
        /// <param name="message">The log message format</param>
        /// <param name="args">The log message object parameters</param>
        private static void LogFormat(this ILog logger, LogLevel logLevel, string message, params object[] args)
        {
            var result = string.Format(CultureInfo.InvariantCulture, message, args);
            logger.Log(logLevel, result.AsFunc());
        }

        /// <summary>
        /// Returns a wrapped func.
        /// </summary>
        /// <typeparam name="T">The current type</typeparam>
        /// <param name="value">The current object</param>
        /// <returns>A function</returns>
        /// <remarks>
        ///     <a href="https://gist.github.com/AArnott/d285feef75c18f6ecd2b">Avoid the closure allocation</a>
        /// </remarks>
        private static Func<T> AsFunc<T>(this T value) where T : class
        {
            return value.Return;
        }

        /// <summary>
        /// Returns the generic type from the object.
        /// </summary>
        /// <typeparam name="T">The current type</typeparam>
        /// <param name="value">The current object</param>
        /// <returns>The {T}</returns>
        private static T Return<T>(this T value)
        {
            return value;
        }

        #endregion
    }
}