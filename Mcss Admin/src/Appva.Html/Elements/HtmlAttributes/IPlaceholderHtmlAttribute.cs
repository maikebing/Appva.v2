// <copyright file="IPlaceholderHtmlAttribute.cs" company="Appva AB">
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
    public interface IPlaceholderHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// A hint to the user of what can be entered in the control . The placeholder text 
        /// must not contain carriage returns or line-feeds. 
        /// </summary>
        /// <param name="hint">The hint text.</param>
        /// <returns>The {T}.</returns>
        [Code("placeholder")]
        T Placeholder(string hint);
    }
}