﻿// <copyright file="Log4NetLogProvider.cs" company="Appva AB">
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
    using System.Linq.Expressions;

    #endregion

    /// <summary>
    /// A Log4Net implementation of a <see cref="ILogProvider"/>.
    /// </summary>
    public class Log4NetLogProvider : ILogProvider
    {
        #region Variables.

        /// <summary>
        /// The logger delegate.
        /// </summary>
        private readonly Func<string, object> loggerByNameDelegate;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetLogProvider"/> class.
        /// </summary>
        public Log4NetLogProvider()
        {
            if (! IsLoggerAvailable())
            {
                throw new InvalidOperationException("log4net.LogManager not found");
            }
            this.loggerByNameDelegate = GetGetLoggerMethodCall();
        }

        #endregion

        #region Public Functions.

        /// <summary>
        /// Returns whether or not the logger is available.
        /// </summary>
        /// <returns>True if log4net is available</returns>
        public static bool IsLoggerAvailable()
        {
            return GetLogManagerType() != null;
        }

        /// <summary>
        /// Returns the <see cref="ILog"/> by name.
        /// </summary>
        /// <param name="name">The logger name</param>
        /// <returns>An <see cref="ILog"/> instance</returns>
        public ILog GetLogger(string name)
        {
            return new Log4NetLogger(this.loggerByNameDelegate(name));
        }

        #endregion

        #region Private Functions.

        /// <summary>
        /// Returns the type of the log provider.
        /// </summary>
        /// <returns>The type</returns>
        private static Type GetLogManagerType()
        {
            return Type.GetType("log4net.LogManager, log4net");
        }

        /// <summary>
        /// Returns the get logger delegate.
        /// </summary>
        /// <returns>A delegate</returns>
        private static Func<string, object> GetGetLoggerMethodCall()
        {
            var logManagerType = GetLogManagerType();
            var method = logManagerType.GetMethod("GetLogger", new[] { typeof(string) });
            var parameter = Expression.Parameter(typeof(string), "name");
            var methodCall = Expression.Call(null, method, parameter);
            return Expression.Lambda<Func<string, object>>(methodCall, parameter).Compile();
        }

        #endregion

        /// <summary>
        /// The log4net logger.
        /// </summary>
        public class Log4NetLogger : ILog
        {
            #region Variables.

            /// <summary>
            /// The logger.
            /// </summary>
            private readonly dynamic logger;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="Log4NetLogger"/> class.
            /// </summary>
            /// <param name="logger">The logger</param>
            internal Log4NetLogger(dynamic logger)
            {
                this.logger = logger;
            }

            #endregion

            #region ILog Members.

            /// <inheritdoc />
            public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception)
            {
                if (messageFunc == null)
                {
                    return this.IsLogLevelEnable(logLevel);
                }
                if (exception != null)
                {
                    return this.LogException(logLevel, messageFunc, exception);
                }
                switch (logLevel)
                {
                    case LogLevel.Info:
                        if (this.logger.IsInfoEnabled)
                        {
                            this.logger.Info(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (this.logger.IsWarnEnabled)
                        {
                            this.logger.Warn(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (this.logger.IsErrorEnabled)
                        {
                            this.logger.Error(messageFunc());
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (this.logger.IsFatalEnabled)
                        {
                            this.logger.Fatal(messageFunc());
                            return true;
                        }
                        break;
                    default:
                        if (this.logger.IsDebugEnabled)
                        {
                            this.logger.Debug(messageFunc()); 
                            //// Log4Net doesn't have a 'Trace' level, so all Trace 
                            //// messages are written as 'Debug'
                            return true;
                        }
                        break;
                }
                return false;
            }

            #endregion

            #region Private Methods.

            /// <summary>
            /// Logs an exception.
            /// </summary>
            /// <param name="logLevel">The <see cref="LogLevel"/></param>
            /// <param name="messageFunc">The message function</param>
            /// <param name="exception">The exception</param>
            /// <returns>True if the logging was successful</returns>
            private bool LogException(LogLevel logLevel, Func<string> messageFunc, Exception exception)
            {
                switch (logLevel)
                {
                    case LogLevel.Info:
                        if (this.logger.IsDebugEnabled)
                        {
                            this.logger.Info(messageFunc(), exception);
                            return true;
                        }
                        break;
                    case LogLevel.Warn:
                        if (this.logger.IsWarnEnabled)
                        {
                            this.logger.Warn(messageFunc(), exception);
                            return true;
                        }
                        break;
                    case LogLevel.Error:
                        if (this.logger.IsErrorEnabled)
                        {
                            this.logger.Error(messageFunc(), exception);
                            return true;
                        }
                        break;
                    case LogLevel.Fatal:
                        if (this.logger.IsFatalEnabled)
                        {
                            this.logger.Fatal(messageFunc(), exception);
                            return true;
                        }
                        break;
                    default:
                        if (this.logger.IsDebugEnabled)
                        {
                            this.logger.Debug(messageFunc(), exception);
                            return true;
                        }
                        break;
                }
                return false;
            }

            /// <summary>
            /// Returns whether or not the input log level is enabled.
            /// </summary>
            /// <param name="logLevel">The <see cref="LogLevel"/> to check</param>
            /// <returns>True if the logLevel is enabled</returns>
            private bool IsLogLevelEnable(LogLevel logLevel)
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        return this.logger.IsDebugEnabled;
                    case LogLevel.Info:
                        return this.logger.IsInfoEnabled;
                    case LogLevel.Warn:
                        return this.logger.IsWarnEnabled;
                    case LogLevel.Error:
                        return this.logger.IsErrorEnabled;
                    case LogLevel.Fatal:
                        return this.logger.IsFatalEnabled;
                    default:
                        return this.logger.IsDebugEnabled;
                }
            }

            #endregion
        }
    }
}
