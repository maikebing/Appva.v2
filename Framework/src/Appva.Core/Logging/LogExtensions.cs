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
namespace Appva.Core.Logging
{
    #region Imports.

    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Validation;
    using Extensions;

    #endregion

    /// <summary>
    /// <see cref="ILog"/> extensions.
    /// </summary>
    public static class LogExtensions
    {
        #region Public Static Methods.

        #region Trace Logging.

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Trace(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, messageFunc);
            }
        }

        /// <summary>
        /// Logs a trace message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Trace(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Trace(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsTraceEnabled())
            {
                logger.LogFormat(LogLevel.Trace, message, args);
            }
        }

        /// <summary>
        /// Logs a trace exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Trace(this ILog logger, Exception exception)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsTraceEnabled())
            {
                logger.Log(LogLevel.Trace, null, exception);
            }
        }

        /// <summary>
        /// Logs a trace message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The log message or message format</param>
        /// <param name="args">The log message object parameters</param>
        public static void Trace(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsTraceEnabled())
            {
                logger.LogFormat(LogLevel.Trace, exception, message, args);
            }
        }

        /// <summary>
        /// Logs a trace message as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void TraceJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsTraceEnabled())
            {
                logger.LogJsonObject(LogLevel.Trace, obj);
            }
        }

        #endregion

        #region Debug Logging.

        
        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Debug(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, messageFunc);
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Debug(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Debug(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsDebugEnabled())
            {
                logger.LogFormat(LogLevel.Debug, message, args);
            }
        }

        /// <summary>
        /// Logs a debug message exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Debug(this ILog logger, Exception exception)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsDebugEnabled())
            {
                logger.Log(LogLevel.Debug, null, exception);
            }
        }

        /// <summary>
        /// Logs a debug message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message or message format</param>
        /// <param name="args">The object arguments</param>
        public static void Debug(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsDebugEnabled())
            {
                logger.LogFormat(LogLevel.Debug, exception, message, args);
            }
        }

        /// <summary>
        /// Logs a debug message as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void DebugJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsDebugEnabled())
            {
                logger.LogJsonObject(LogLevel.Debug, obj);
            }
        }

        #endregion

        #region Information Logging.

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Info(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, messageFunc);
            }
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Info(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Info(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsInfoEnabled())
            {
                logger.LogFormat(LogLevel.Info, message, args);
            }
        }

        /// <summary>
        /// Logs an information exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Info(this ILog logger, Exception exception)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsInfoEnabled())
            {
                logger.Log(LogLevel.Info, null, exception);
            }
        }

        /// <summary>
        /// Logs an information message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message or message format</param>
        /// <param name="args">The object arguments</param>
        public static void Info(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsInfoEnabled())
            {
                logger.LogFormat(LogLevel.Info, exception, message, args);
            }
        }

        /// <summary>
        /// Logs an information message as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void InfoJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsInfoEnabled())
            {
                logger.LogJsonObject(LogLevel.Info, obj);
            }
        }

        #endregion

        #region Warning Logging.

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Warn(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, messageFunc);
            }
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Warn(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Warn(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsWarnEnabled())
            {
                logger.LogFormat(LogLevel.Warn, message, args);
            }
        }

        /// <summary>
        /// Logs a warning exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Warn(this ILog logger, Exception exception)
        {
            if (logger.IsWarnEnabled())
            {
                logger.Log(LogLevel.Warn, null, exception);
            }
        }

        /// <summary>
        /// Logs a warning message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message or message format</param>
        /// <param name="args">The object arguments</param>
        public static void Warn(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsWarnEnabled())
            {
                logger.LogFormat(LogLevel.Warn, exception, message, args);
            }
        }

        /// <summary>
        /// Logs a warning object as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void WarnJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsWarnEnabled())
            {
                logger.LogJsonObject(LogLevel.Warn, obj);
            }
        }

        #endregion

        #region Error Logging.

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Error(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, messageFunc);
            }
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Error(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Error(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsErrorEnabled())
            {
                logger.LogFormat(LogLevel.Error, message, args);
            }
        }

        /// <summary>
        /// Logs an error exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Error(this ILog logger, Exception exception)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsErrorEnabled())
            {
                logger.Log(LogLevel.Error, null, exception);
            }
        }

        /// <summary>
        /// Logs an error message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message or message format</param>
        /// <param name="args">The object arguments</param>
        public static void Error(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsErrorEnabled())
            {
                logger.LogFormat(LogLevel.Error, exception, message, args);
            }
        }

        /// <summary>
        /// Logs an error object as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void ErrorJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsErrorEnabled())
            {
                logger.LogJsonObject(LogLevel.Error, obj);
            }
        }

        #endregion

        #region Fatal Logging.

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="messageFunc">The log message function, e.g. () => string.Format(x, "a")</param>
        public static void Fatal(this ILog logger, Func<string> messageFunc)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, messageFunc);
            }
        }

        /// <summary>
        /// Logs a fatal message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="message">The log message</param>
        public static void Fatal(this ILog logger, string message)
        {
            Requires.NotNull(logger, "logger");
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
        public static void Fatal(this ILog logger, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsFatalEnabled())
            {
                logger.LogFormat(LogLevel.Fatal, message, args);
            }
        }

        /// <summary>
        /// Logs a fatal exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        public static void Fatal(this ILog logger, Exception exception)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsFatalEnabled())
            {
                logger.Log(LogLevel.Fatal, null, exception);
            }
        }

        /// <summary>
        /// Logs a fatal message with the exception.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="exception">The exception</param>
        /// <param name="message">The message or message format</param>
        /// <param name="args">The object arguments</param>
        public static void Fatal(this ILog logger, Exception exception, string message, params object[] args)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsFatalEnabled())
            {
                logger.LogFormat(LogLevel.Fatal, exception, message, args);
            }
        }

        /// <summary>
        /// Logs a fatal object as json.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="obj">The log object</param>
        public static void FatalJson(this ILog logger, object obj)
        {
            Requires.NotNull(logger, "logger");
            if (logger.IsFatalEnabled())
            {
                logger.LogJsonObject(LogLevel.Fatal, obj);
            }
        }

        #endregion

        #endregion

        #region Private Static Methods.

        /// <summary>
        /// Formats the log message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="logLevel">The <see cref="LogLevel"/></param>
        /// <param name="message">The log message format</param>
        /// <param name="args">The log message object parameters</param>
        private static void LogFormat(this ILog logger, LogLevel logLevel, string message, params object[] args)
        {
            LogFormat(logger, logLevel, null, message, args);
        }

        /// <summary>
        /// Formats the log message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="logLevel">The <see cref="LogLevel"/></param>
        /// <param name="exception">The log exception</param>
        /// <param name="message">The log message format</param>
        /// <param name="args">The log message object parameters</param>
        private static void LogFormat(this ILog logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            if (logLevel == LogLevel.Trace || logLevel == LogLevel.Debug)
            {

            }
            logger.Log(logLevel, string.Format(CultureInfo.InvariantCulture, message, args).AsFunc(), exception);
        }

        /// <summary>
        /// Formats the log message.
        /// </summary>
        /// <param name="logger">The current <see cref="ILog"/></param>
        /// <param name="logLevel">The <see cref="LogLevel"/></param>
        /// <param name="obj">The log message object</param>
        private static void LogJsonObject(this ILog logger, LogLevel logLevel, object obj)
        {
            Func<string> message;
            if (obj == null)
            {
                message = string.Empty.AsFunc();
            }
            else
            {
                try
                {
                    message = JsonConvert.SerializeObject(obj).AsFunc();
                }
                catch (Exception)
                {
                    message = obj.GetType().AssemblyQualifiedName.AsFunc();
                }
            }
            logger.Log(logLevel, message);
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