// <copyright file="DependencyResolver.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
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
        /// Initializes a new instance of the <see cref="MediatorDependencyResolver"/> class.
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