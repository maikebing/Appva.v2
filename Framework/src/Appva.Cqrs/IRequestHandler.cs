// <copyright file="IRequestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cqrs
{
    #region Imports.

    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Handlers are orchestrations of a use case from start to finish.
    /// </summary>
    /// <typeparam name="TRequest">The message request type</typeparam>
    /// <typeparam name="TResponse">The message response type</typeparam>
    public interface IRequestHandler<in TRequest, out TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Handles a request message.
        /// </summary>
        /// <param name="message">The request message</param>
        /// <returns>A {TResponse} message</returns>
        TResponse Handle(TRequest message);
    }

    /// <summary>
    /// In order to invoke 'Handle' for any message type.
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// Handles a request message.
        /// </summary>
        /// <param name="message">The request message</param>
        /// <returns>A response message as object</returns>
        object Handle(object message);
    }

    /// <summary>
    /// Handlers are orchestrations of a use case from start to finish.
    /// </summary>
    /// <typeparam name="TRequest">The message request type</typeparam>
    /// <typeparam name="TResponse">The message response type</typeparam>
    public interface IAsyncRequestHandler<in TRequest, TResponse> : IAsyncRequestHandler
        where TRequest : IAsyncRequest<TResponse>
    {
        /// <summary>
        /// Handles a request message asyncronous.
        /// </summary>
        /// <param name="message">The request message</param>
        /// <returns>A Task{TResponse} message</returns>
        Task<TResponse> Handle(TRequest message);
    }

    /// <summary>
    /// In order to invoke 'Handle' for any message type.
    /// </summary>
    public interface IAsyncRequestHandler
    {
        /// <summary>
        /// Handles a request message.
        /// </summary>
        /// <param name="message">The request message</param>
        /// <returns>A Task response message as object</returns>
        Task<object> Handle(object message);
    }

    /// <summary>
    /// Abstract base <see cref="IRequestHandler{TRequest, TResponse}"/> implementation.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region IRequestHandler<TRequest,TResponse> Members.

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

    /// <summary>
    /// Abstract base <see cref="IAsyncRequestHandler{TRequest, TResponse}"/> implementation.
    /// </summary>
    /// <typeparam name="TRequest">The request type</typeparam>
    /// <typeparam name="TResponse">The response type</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public abstract class AsyncRequestHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {
        #region IAsyncRequestHandler<TRequest,TResponse> Members

        /// <inheritdoc />
        public abstract Task<TResponse> Handle(TRequest message);

        #endregion

        #region IAsyncRequestHandler Members

        /// <inheritdoc />
        public async Task<object> Handle(object message)
        {
            return await this.Handle((TRequest) message).ConfigureAwait(false) as Task<object>;
        }

        #endregion
    }
}
