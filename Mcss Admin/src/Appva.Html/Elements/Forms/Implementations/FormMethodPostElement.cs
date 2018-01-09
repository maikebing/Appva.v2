// <copyright file="FormMethodPostElement.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FormMethodPostElement : FormElement<IFormMethodPost>, IFormMethodPost
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMethodPostElement"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="HtmlHelper"/>.</param>
        /// <param name="route">The <see cref="IRoute"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="htmlHelper"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <typeparamref name="route"/> is null.
        /// </exception>
        public FormMethodPostElement(HtmlHelper helper, IRoute route)
            : base(HttpVerbs.Post, helper, route)
        {
        }

        #endregion
    }
}