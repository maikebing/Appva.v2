// <copyright file="DispatchAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Attributes
{
    #region Imports.

    using System;
    using System.Web.Mvc;
    using Core.Extensions;
    using Cqrs;
    //using Validation;

    #endregion

    /// <summary>
    /// UI mediator/bus to facilitate request/response dispatch.
    /// </summary>
    /// <remarks>This is executed after validate</remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class DispatchAttribute : ActionFilterAttribute
    {
        #region Variables.

        /// <summary>
        /// Empty request type.
        /// </summary>
        private readonly Type requestType;

        /// <summary>
        /// The redirect action.
        /// </summary>
        private readonly string action;

        /// <summary>
        /// The redirect controller.
        /// </summary>
        private readonly string controller;

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
            : this(null, null, null, AttributeAction.CreateRequestByParameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// The auto dispatch will create a new instance of the requestType and invoke the
        /// implemented handler.
        /// </summary>
        /// <param name="requestType">Empty request type</param>
        public DispatchAttribute(Type requestType)
            : this(requestType, null, null, AttributeAction.CreateRequestAutomatically)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// The auto dispatch will resolve the "request" action parameter and invoke the
        /// implemented handler, redirect to an action and inject the
        /// result parameters.
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="controller">The controller</param>
        public DispatchAttribute(string action, string controller)
            : this(null, action, controller, AttributeAction.CreateRequestByParameterAndRedirect)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchAttribute"/> class.
        /// </summary>
        /// <param name="requestType">Empty request type</param>
        /// <param name="action">The action</param>
        /// <param name="controller">The controller</param>
        /// <param name="attributeAction">The attribute action</param>
        private DispatchAttribute(Type requestType, string action, string controller, AttributeAction attributeAction)
        {
            this.Order = 1000;
            this.requestType = requestType;
            this.action = action;
            this.controller = controller;
            this.attributeAction = attributeAction;
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

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            switch (this.attributeAction)
            {
                case AttributeAction.CreateRequestByParameter:
                    this.CreateRequestByParameter(filterContext);
                    break;
                case AttributeAction.CreateRequestByParameterAndRedirect:
                    this.CreateRequestByParameterAndRedirect(filterContext);
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
        private void CreateRequestByParameter(ActionExecutingContext filterContext)
        {
            //Requires.ValidState(filterContext.ActionParameters.ContainsKey("request"), "Parameter 'request' does not exist");
            var request = filterContext.ActionParameters["request"];
            filterContext.Controller.ViewData.Model = this.Handler(request).Handle(request);
        }

        /// <summary>
        /// Creates the request by the controller parameter
        /// and redirects to action.
        /// </summary>
        /// <param name="filterContext">The context</param>
        private void CreateRequestByParameterAndRedirect(ActionExecutingContext filterContext)
        {
            //Requires.ValidState(filterContext.ActionParameters.ContainsKey("request"), "Parameter 'request' does not exist");
            var request = filterContext.ActionParameters["request"];
            var routeValues = this.Handler(request).Handle(request);
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var url = urlHelper.Action(this.action, this.controller, routeValues);
            filterContext.Result = new RedirectResult(url);
        }

        /// <summary>
        /// Creates the request by the controller parameter
        /// and redirects to action.
        /// </summary>
        /// <param name="filterContext">The context</param>
        private void CreateRequestByParameterOrRedirect(ActionExecutingContext filterContext)
        {
            //Requires.ValidState(filterContext.ActionParameters.ContainsKey("request"), "Parameter 'request' does not exist");
            var request = filterContext.ActionParameters["request"];
            var routeValues = this.Handler(request).Handle(request);
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            var url = urlHelper.Action(this.action, this.controller, routeValues);
            filterContext.Result = new RedirectResult(url);
        }

        /// <summary>
        /// Creates the request automatically.
        /// </summary>
        /// <param name="filterContext">The context</param>
        private void CreateRequestAutomatically(ActionExecutingContext filterContext)
        {
            var request = Activator.CreateInstance(this.requestType);
            var handler = this.Handler(request);
            filterContext.Controller.ViewData.Model = handler.Handle(request);
        }

        /// <summary>
        /// Creates the handler from the request.
        /// </summary>
        /// <param name="request">The request to be handled</param>
        /// <returns>A <see cref="IRequestHandler"/></returns>
        private IRequestHandler Handler(object request)
        {
            var modelType = request.GetType().GetInterfaces()[0].GetGenericArguments()[0];
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), modelType);
            var handler = DependencyResolver.Current.GetService(handlerType);
            //Requires.ValidState(handler != null, "Handler {0} was not found for request".FormatWith(handlerType));
            return handler as IRequestHandler;
        }

        #endregion
    }
}