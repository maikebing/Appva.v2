// <copyright file="Range.cs" company="Appva AB">
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
    /// A control for entering a number whose exact value is not important.
    /// </summary>
    public interface IRange : IHtmlElement<IRange>, IInputElement<IRange>, IListHtmlAttribute<IRange>,
        IMinHtmlAttribute<IRange>, IMaxHtmlAttribute<IRange>, IStepHtmlAttribute<IRange>, IRequiredHtmlAttribute<IRange>
    {
    }

    /// <summary>
    /// An <see cref="IRange"/> implementation.
    /// </summary>
    internal sealed class Range : Input<IRange>, IRange
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Range"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        public Range(HtmlHelper htmlHelper)
            : base(InputType.Range, htmlHelper)
        {
        }

        #endregion
    }
}