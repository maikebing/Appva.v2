// <copyright file="Mediator.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cqrs
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        TResponse Send<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        void Publish<TNotification>(TNotification notification) where TNotification : INotification;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        /// <returns></returns>
        Task PublishAsync<TNotification>(TNotification notification) where TNotification : IAsyncNotification;
    }

    /// <summary>
    /// Intermediary to decouple many peers.
    /// </summary>
    public class Mediator : IMediator
    {
        #region Variables.

        /// <summary>
        /// The service locator.
        /// </summary>
        private readonly IDependencyResolver dependencyResolver;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="dependencyResolver">The <see cref="IDependencyResolver"/></param>
        public Mediator(IDependencyResolver dependencyResolver)
        {
            this.dependencyResolver = dependencyResolver;
        }

        #endregion

        /// <inheritdoc />
        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = this.dependencyResolver.GetInstance(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException("Handler was not found for request of type " + handlerType.GetType());
            }
            return (TResponse) this.GetHandlerMethod(handlerType, "Handle", request.GetType()).Invoke(handler, new[] { request });
        }

        /// <inheritdoc />
        public async Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            var handlerType = typeof(IAsyncRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = this.dependencyResolver.GetInstance(handlerType);
            if (handler == null)
            {
                throw new InvalidOperationException("Handler was not found for request of type " + handlerType.GetType());
            }
            return await (Task<TResponse>) this.GetHandlerMethod(handlerType, "Handle", request.GetType()).Invoke(handler, new[] { request });
        }

        /// <inheritdoc />
        public void Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            var notificationHandlers = this.NotificationHandlers<TNotification>();
            foreach (var handler in notificationHandlers)
            {
                handler.Handle(notification);
            }
        }

        /// <inheritdoc />
        public async Task PublishAsync<TNotification>(TNotification notification) where TNotification : IAsyncNotification
        {
            var notificationHandlers = this.AsyncNotificationHandlers<TNotification>();
            foreach (var handler in notificationHandlers)
            {
                await handler.Handle(notification);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        private IEnumerable<INotificationHandler<TNotification>> NotificationHandlers<TNotification>()
            where TNotification : INotification
        {
            return this.dependencyResolver.GetInstances<INotificationHandler<TNotification>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <returns></returns>
        private IEnumerable<IAsyncNotificationHandler<TNotification>> AsyncNotificationHandlers<TNotification>()
            where TNotification : IAsyncNotification
        {
            return this.dependencyResolver.GetInstances<IAsyncNotificationHandler<TNotification>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerType"></param>
        /// <param name="handlerMethodName"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        private MethodInfo GetHandlerMethod(Type handlerType, string handlerMethodName, Type messageType)
        {
            return handlerType
                .GetMethod(handlerMethodName,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                    null, CallingConventions.HasThis,
                    new[] { messageType },
                    null);
        }
    }
}