// <copyright file="AuthorizedBlock.cs" company="Appva AB">
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
    using Appva.Core.Contracts.Permissions;
    using Appva.Html.Infrastructure;
    using Appva.Html.Infrastructure.Internal;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AuthorizedBlock<T> : Block<T> where T : class, IHtmlElement<T>
    {
        #region Variables.

        private readonly AuthorizationState state;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizedBlock{T}"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="tag">The <see cref="Tag"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="tag"/> is null.
        /// </exception>
        protected AuthorizedBlock(HtmlHelper htmlHelper, Tag tag)
            : base(htmlHelper, tag)
        {
            this.state = AuthorizationState.Authorized;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizedBlock{T}"/> class.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="tag">The <see cref="Tag"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="route"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="tag"/> is null.
        /// </exception>
        protected AuthorizedBlock(IPermission permission, HtmlHelper htmlHelper, Tag tag)
            : base(htmlHelper, tag)
        {
            Argument.Guard.NotNull("permission", permission);
            this.state = htmlHelper.HasPermissionFor(permission) ? AuthorizationState.Authorized : AuthorizationState.UnAuthorized;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizedBlock{T}"/> class.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="tag">The <see cref="Tag"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="route"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="tag"/> is null.
        /// </exception>
        protected AuthorizedBlock(IRoute route, HtmlHelper htmlHelper, Tag tag)
            : base(htmlHelper, tag)
        {
            Argument.Guard.NotNull("route", route);
            this.state = route.State;
        }

        #endregion

        /// <inheritdoc />
        protected override bool IsAuthorized()
        {
            return this.state == AuthorizationState.Authorized; 
        }
    }
}