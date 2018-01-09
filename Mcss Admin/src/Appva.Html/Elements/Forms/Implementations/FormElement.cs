// <copyright file="FormElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Abstract base class for html forms.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    internal abstract class FormElement<T> : AuthorizedBlock<T>, IForm<T> where T : class, IHtmlElement<T>
    {
        /// <summary>
        /// The route.
        /// </summary>
        private readonly IRoute route;

        /// <summary>
        /// The form method.
        /// </summary>
        private readonly HttpVerbs method;

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormElement{T}"/> class.
        /// </summary>
        /// <param name="method">
        /// The form method.
        /// </param>
        /// <param name="htmlHelper">
        /// The <see cref="HtmlHelper"/>.
        /// </param>
        /// <param name="route">
        /// The <see cref="IRoute"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="route"/> is null.
        /// </exception>
        protected FormElement(HttpVerbs method, HtmlHelper htmlHelper, IRoute route)
            : base(route, htmlHelper, Tag.New("form"))
        {
            Argument.Guard.NotNull("route", route);
            this.route  = route;
            this.method = method;
            this.Id(this.GenerateId());
            this.Method(this.method);
            this.Name(this.Builder.TagName + "-" + this.route.Key.Value.Replace(".", "-"));
            this.Action(this.route);
            this.DisableValidation();
            this.Charset(FormCharset.Utf8);
            this.Role(this.Builder.TagName);
            this.Class("form", "validate");
        }

        #endregion

        #region IFormAttributes<T> Members.

        /// <inheritdoc />
        public T Fragment(string id)
        {
            this.Action(this.route, id);
            return this as T;
        }

        #endregion

        #region Internal Members.

        /// <summary>
        /// The URI of a program that processes the form information. This value can be 
        /// overridden by a formaction attribute on a button or input element.
        /// </summary>
        /// <param name="route">The <see cref="IRoute"/>.</param>
        /// <param name="fragment">Optional fragment identifier for the url.</param>
        /// <example>
        /// <code language="html" title="Action Example">
        ///     <form action="foo/bar/create"></form>
        /// </code>
        /// </example>
        internal void Action(IRoute route, string fragment = null)
        {
            if (string.IsNullOrWhiteSpace(fragment))
            {
                return;
            }
            this.Builder.MergeAttribute("action", route.Url + "#" + fragment, true);
        }

        /// <summary>
        /// The HTTP method that the browser uses to submit the form. Possible values are:
        /// post: Corresponds to the HTTP POST method ; form data are included in the body 
        /// of the form and sent to the server.
        /// get: Corresponds to the HTTP GET method; form data are appended to the action 
        /// attribute URI with a '?' as separator, and the resulting URI is sent to the 
        /// server. Use this method when the form has no side-effects and contains only 
        /// ASCII characters.
        /// This value can be overridden by a formmethod attribute on a button or input 
        /// element.
        /// </summary>
        /// <param name="method">The http method.</param>
        internal void Method(HttpVerbs method)
        {
            this.Builder.MergeAttribute("method", method == HttpVerbs.Get ? "get" : "post", true);
        }

        #endregion

        #region Protected Members.

        /// <inheritdoc />
        protected override void OnBeforeBegin()
        {
            this.ViewContext.FormContext = new FormContext();
        }

        /// <inheritdoc />
        protected override void OnBeforeEnd()
        {
            if (this.method != HttpVerbs.Post && this.method != HttpVerbs.Get)
            {
                this.Write(this.Html.HttpMethodOverride(this.method));
            }
            this.Write(this.Html.AntiForgeryToken());
        }

        /// <inheritdoc />
        protected override void OnEnd()
        {
            this.ViewContext.OutputClientValidation();
            this.ViewContext.FormContext = null;
        }

        #endregion
    }
}