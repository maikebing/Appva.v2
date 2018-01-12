// <copyright file="ICaptureHtmlAttribute.cs" company="Appva AB">
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
    public interface ICaptureHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// When the value of the type attribute is file, the presence of this Boolean 
        /// attribute indicates that capture of media directly from the device's environment 
        /// using a media capture mechanism is preferred.
        /// </summary>
        /// <returns>The {T}.</returns>
        [Code("capture", IsNoValue = true)]
        T Capture();
    }
}