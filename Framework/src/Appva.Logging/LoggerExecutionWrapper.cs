// <copyright file="LoggerExecutionWrapper.cs" company="Appva AB">
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

    #endregion

    /// <summary>
    /// An <see cref="ILog"/> wrapper.
    /// </summary>
    public class LoggerExecutionWrapper : ILog
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/> instance.
        /// </summary>
        private readonly ILog logger;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerExecutionWrapper"/> class.
        /// </summary>
        /// <param name="logger">An <see cref="ILog"/> instance</param>
        public LoggerExecutionWrapper(ILog logger)
        {
            this.logger = logger;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the <see cref="ILog"/> instance.
        /// </summary>
        public ILog WrappedLogger
        {
            get
            {
                return this.logger;
            }
        }

        #endregion

        #region ILog Members.

        /// <inheritdoc />
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            if (messageFunc == null)
            {
                return this.logger.Log(logLevel, null);
            }
            Func<string> wrappedMessageFunc = () =>
            {
                try
                {
                    return messageFunc();
                }
                catch (Exception ex)
                {
                    this.Log(LogLevel.Error, () => "Failed to generate log message", ex);
                }
                return null;
            };
            return this.logger.Log(logLevel, wrappedMessageFunc, exception);
        }

        #endregion
    }
}
