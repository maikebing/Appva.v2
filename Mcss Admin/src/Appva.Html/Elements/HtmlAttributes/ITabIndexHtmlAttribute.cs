// <copyright file="ITabIndexHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Is an integer attribute indicating if the element can take input focus (is 
    /// focusable), if it should participate to sequential keyboard navigation, and if 
    /// so, at what position.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface ITabIndexHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is an integer attribute indicating if the element can take input focus (is 
        /// focusable), if it should participate to sequential keyboard navigation, and if 
        /// so, at what position. It can takes several values:
        /// a negative value means that the element should be focusable, but should not be 
        /// reachable via sequential keyboard navigation;
        /// 0 means that the element should be focusable and reachable via sequential 
        /// keyboard navigation, but its relative order is defined by the platform 
        /// convention;
        /// a positive value which means should be focusable and reachable via sequential 
        /// keyboard navigation; its relative order is defined by the value of the 
        /// attribute: the sequential follow the increasing number of the tabindex. If 
        /// several elements share the same tabindex, their relative order follows their 
        /// relative position in the document).
        /// </summary>
        /// <param name="value">
        /// The order of input focus.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("tabindex")]
        T TabIndex(int value);
    }
}