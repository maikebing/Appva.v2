// <copyright file="DispatchAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure
{
    #region Imports.

    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Appva.WebApi.Filters;
    using Core.Extensions;
    using Cqrs;
    using Validation;

    #endregion

    /// <summary>
    /// UI mediator/bus to facilitate request/response dispatch.
    /// </summary>
    /// <remarks>This is executed after validate</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class DispatchAttribute : ActionFilterAttribute, IOrderedFilter
    {
        #region Variables.

        /// <summary>
        /// Empty request type.
        /// </summary>
        private readonly Type requestType;

        /// <summary>
        /// The dispatch action.
        /// </summary>
        private readonly AttributeAction attributeAction;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// The auto dispatch will resolve the "request" action parameter and invoke the
        /// implemented handler.
        /// </summary>
        public DispatchAttribute()
            : this(null, AttributeAction.CreateRequestByParameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// The auto dispatch will create a new instance of the requestType and invoke the
        /// implemented handler.
        /// </summary>
        /// <param name="requestType">Empty request type</param>
        public DispatchAttribute(Type requestType)
            : this(requestType, AttributeAction.CreateRequestAutomatically)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// </summary>
        /// <param name="requestType">Empty request type</param>
        /// <param name="attributeAction">The attribute action</param>
        private DispatchAttribute(Type requestType, AttributeAction attributeAction)
        {
            this.requestType = requestType;
            this.attributeAction = attributeAction;
            this.Order = 500;
        }

        #endregion

        #region Private Enums.

        /// <summary>
        /// Dispatcher action.
        /// </summary>
        private enum AttributeAction
        {
            /// <summary>
            /// Creates the request by the controller parameter.
            /// </summary>
            CreateRequestByParameter,

            /// <summary>
            /// Creates the request by the controller parameter
            /// and redirects to action.
            /// </summary>
            CreateRequestByParameterAndRedirect,

            /// <summary>
            /// Creates the request automatically.
            /// </summary>
            CreateRequestAutomatically
        }

        #endregion

        #region IOrderedFilter Members

        public int Order
        {
            get;
            set;
        }

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            switch (this.attributeAction)
            {
                case AttributeAction.CreateRequestByParameter:
                    this.CreateRequestByParameter(filterContext);
                    break;
                case AttributeAction.CreateRequestAutomatically:
                    this.CreateRequestAutomatically(filterContext);
                    break;
            }
            base.OnActionExecuting(filterContext);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates the request by the controller parameter.
        /// </summary>
        /// <param name="filterContext">The context</param>
        private void CreateRequestByParameter(HttpActionContext filterContext)
        {
            Requires.ValidState(filterContext.ActionArguments.ContainsKey("request"), "Parameter 'request' does not exist");
            var request = filterContext.ActionArguments["request"];
            filterContext.Response = filterContext.Request.CreateResponse(this.Handler(filterContext, request).Handle(request));
        }

        /// <summary>
        /// Creates the request automatically.
        /// </summary>
        /// <param name="filterContext">The context</param>
        private void CreateRequestAutomatically(HttpActionContext filterContext)
        {
            var request = Activator.CreateInstance(this.requestType);
            var handler = this.Handler(filterContext, request);
            filterContext.Response = filterContext.Request.CreateResponse(handler.Handle(request));
        }

        /// <summary>
        /// Creates the handler from the request.
        /// </summary>
        /// <param name="request">The request to be handled</param>
        /// <returns>A <see cref="IRequestHandler"/></returns>
        private IRequestHandler Handler(HttpActionContext filterContext, object request)
        {
            var modelType = request.GetType().GetInterfaces()[0].GetGenericArguments()[0];
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), modelType);
            var handler = filterContext.Request.GetDependencyScope().GetService(handlerType);
            Requires.ValidState(handler != null, "Handler {0} was not found for request".FormatWith(handlerType));
            return handler as IRequestHandler;
        }

        #endregion
    }
}