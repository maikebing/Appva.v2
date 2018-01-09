// <copyright file="Search.cs" company="Appva AB">
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
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// A single-line text field for entering search strings. Line-breaks are 
    /// automatically removed from the input value.
    /// </summary>
    public interface ISearch : IHtmlElement<ISearch>, IInputElement<ISearch>, IListHtmlAttribute<ISearch>,
        IMaxLengthHtmlAttribute<ISearch>, IMinLengthHtmlAttribute<ISearch>, IPatternHtmlAttribute<ISearch>,
        IReadonlyHtmlAttribute<ISearch>, IRequiredHtmlAttribute<ISearch>, ISizeHtmlAttribute<ISearch>
    {
    }

    /// <summary>
    /// An <see cref="ISearch"/> implementation.
    /// </summary>
    internal sealed class Search : Input<ISearch>, ISearch
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Search(HtmlHelper htmlHelper)
            : base(InputType.Search, htmlHelper)
        {
        }

        #endregion
    }
}