// <copyright file="AuthorizedBlock.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Core.Contracts.Permissions;
    using Appva.Html.Infrastructure.Internal;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AuthorizedBlock<T> : Block<T> where T : class
    {
        #region Variables.

        private readonly IRoute route;

        /// <summary>
        /// The saved state of the view context writer.
        /// </summary>
        private StringBuilder saved;

        #endregion

        #region Constructor.

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
        public AuthorizedBlock(IRoute route, HtmlHelper htmlHelper, Tag tag)
            : base(htmlHelper, tag)
        {
            this.route = route;
        }

        #endregion

        protected override void Initialize()
        {
            if (this.route.State == State.UnAuthorized)
            {
                this.saved = new StringBuilder();
                this.saved.Append(((StringWriter) this.Html.ViewContext.Writer).GetStringBuilder());
                return;
            }
            base.Initialize();
        }

        protected override void Finalize()
        {
            if (this.route.State == State.UnAuthorized)
            {
                var writer = (StringWriter) this.Html.ViewContext.Writer;
                writer.GetStringBuilder().Length = 0;
                writer.GetStringBuilder().Append(this.saved);
                return;
            }
            base.Finalize();
        }
    }
}