// <copyright file="IMultipleHtmlAttribute.cs" company="Appva AB">
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
    public interface IMultipleHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// This Boolean attribute indicates whether the user can enter more than one value. 
        /// This attribute applies when the type attribute is set to email or file, 
        /// otherwise it is ignored.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("multiple")]
        T Multiple();
    }
}