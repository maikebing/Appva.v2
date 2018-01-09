// <copyright file="ITitleHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Contains a text representing advisory information related to the element it 
    /// belongs to.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface ITitleHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Contains a text representing advisory information related to the element it 
        /// belongs to. Such information can typically, but not necessarily, be presented to 
        /// the user as a tooltip.
        /// </summary>
        /// <param name="value">
        /// The title tooltip.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("title")]
        T Title(string value);
    }
}