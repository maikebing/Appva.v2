// <copyright file="IDisabledHtmlAttribute.cs" company="Appva AB">
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
    public interface IDisabledHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This Boolean attribute indicates that the form control is not available for 
        /// interaction. In particular, the click event will not be dispatched on disabled 
        /// controls. Also, a disabled control's value isn't submitted with the form.
        /// </summary>
        /// <param name="disabled">
        /// If disabled condition.
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("disabled")]
        T IsDisabled(bool disabled);
    }
}