﻿// <copyright file="ILog.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// Simple interface that represent a logger.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Log a message the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="messageFunc">The message function.</param>
        /// <param name="exception">An optional exception.</param>
        /// <returns>true if the message was logged. Otherwise false.</returns>
        /// <remarks>
        /// Note to implementers: the message func should not be called if the loglevel is 
        /// not enabled so as not to incur performance penalties.
        /// To check IsEnabled call Log with only LogLevel and check the return value, no 
        /// event will be written
        /// </remarks>
        bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null);

        /// <summary>
        /// Whether or not trace is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if trace is enabled</returns>
        bool IsTraceEnabled();

        /// <summary>
        /// Whether or not debug is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if debug is enabled</returns>
        bool IsDebugEnabled();

        /// <summary>
        /// Whether or not info is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if info is enabled</returns>
        bool IsInfoEnabled();

        /// <summary>
        /// Whether or not warn is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if warn is enabled</returns>
        bool IsWarnEnabled();

        /// <summary>
        /// Whether or not error is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if error is enabled</returns>
        bool IsErrorEnabled();

        /// <summary>
        /// Whether or not fatal is enabled for this <see cref="ILog"/>.
        /// </summary>
        /// <returns>True if fatal is enabled</returns>
        bool IsFatalEnabled();
    }
}