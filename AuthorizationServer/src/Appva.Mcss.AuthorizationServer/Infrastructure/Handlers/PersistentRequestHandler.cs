// <copyright file="PersistentRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Cqrs;
    using Persistence;

    #endregion

    /// <summary>
    /// An abstract requesst handler with persistence.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    internal abstract class PersistentRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Variables.

        /// <summary>
        /// Injected <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentRequestHandler{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PersistentRequestHandler(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the <see cref="IPersistenceContext"/>.
        /// </summary>
        protected IPersistenceContext Persistence
        {
            get
            {
                return this.persistenceContext;
            }
        }

        #endregion

        #region IRequestHandler<TRequest, TResponse> Members.

        /// <inheritdoc />
        public abstract TResponse Handle(TRequest message);

        #endregion

        #region IRequestHandler Members.

        /// <inheritdoc />
        public object Handle(object message)
        {
            return this.Handle((TRequest) message);
        }

        #endregion
    }
}