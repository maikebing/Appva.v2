// <copyright file="ICheckedHtmlAttribute.cs" company="Appva AB">
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
    public interface ICheckedHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// When the value of the type attribute is radio or checkbox, the presence of this 
        /// Boolean attribute indicates that the control is selected by default, otherwise 
        /// it is ignored.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("checked", IsNoValue = true)]
        T Checked();
    }
}