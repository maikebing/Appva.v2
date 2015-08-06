// <copyright file="DependencyResolver.cs" company="Appva AB">
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
    using System.Collections.Generic;
    using Autofac;

    #endregion

    /// <summary>
    /// A Service Locator.
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Returns an instance by type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The object</returns>
        object GetInstance(Type type);

        /// <summary>
        /// Returns all instances.
        /// </summary>
        /// <typeparam name="T">The type parameter</typeparam>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> GetInstances<T>();
    }

    /// <summary>
    /// Autofac dependency resolver.
    /// </summary>
    public class MediatorDependencyResolver : IDependencyResolver
    {
        #region Variables.

        /// <summary>
        /// The life time scope.
        /// </summary>
        private readonly ILifetimeScope container;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorDependencyResolver"/> 
        /// class.
        /// </summary>
        /// <param name="container">A <see cref="ILifetimeScope"/></param>
        public MediatorDependencyResolver(ILifetimeScope container)
        {
            this.container = container;
        }

        #endregion

        #region IDependencyResolver Implementation.

        /// <inheritdoc />
        public object GetInstance(Type type)
        {
            return this.container.Resolve(type);
        }

        /// <inheritdoc />
        public IEnumerable<T> GetInstances<T>()
        {
            return this.container.Resolve<IEnumerable<T>>();
        }

        #endregion
    }
}
