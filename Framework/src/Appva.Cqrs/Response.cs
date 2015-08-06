// <copyright file="Response.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// Copyright (c) 2013 Matt Hinze
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
namespace Appva.Cqrs
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Response : Response<Unit>
    {
        #region Response<Unit> Overrides.

        /// <summary>
        /// Returns default unit.
        /// </summary>
        public override Unit Data
        {
            get
            {
                return Unit.Value;
            }
        }

        #endregion
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="TResponseData">The response data</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Response<TResponseData>
    {
        /// <summary>
        /// The message response data.
        /// </summary>
        public virtual TResponseData Data
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Exception"/>.
        /// </summary>
        public virtual Exception Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the response has an exception.
        /// </summary>
        /// <returns>True if an <see cref="Exception"/> was raised</returns>
        public virtual bool HasException()
        {
            return this.Exception != null;
        }
    }
}
