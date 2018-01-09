// <copyright file="IHiddenHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IHiddenHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Is a Boolean attribute indicates that the element is not yet, or is no longer, 
        /// relevant. For example, it can be used to hide elements of the page that can't be 
        /// used until the login process has been completed. The browser won't render such 
        /// elements. This attribute must not be used to hide content that could 
        /// legitimately be shown.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("dropzone", IsNoValue = true)]
        T Hidden();
    }
}