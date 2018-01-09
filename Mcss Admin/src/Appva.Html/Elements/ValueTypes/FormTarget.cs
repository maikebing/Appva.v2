// <copyright file="FormTarget.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Represents a value for <see cref="IFormHtmlAttributes{T}.Target"/>.
    /// </summary>
    public enum FormTarget
    {
        /// <summary>
        /// Load the response into the same HTML 4 frame (or HTML5 browsing context) 
        /// as the current one. This value is the default if the attribute is not specified.
        /// </summary>
        /// <example>
        /// <code language="html" title="Target Self Example">
        ///     <form target="_self"></form>
        /// </code>
        /// </example>
        [Code("_self")]
        Self,

        /// <summary>
        /// Load the response into a new unnamed HTML 4 window or HTML5 browsing 
        /// context.
        /// </summary>
        /// <example>
        /// <code language="html" title="Target Blank Example">
        ///     <form target="_blank"></form>
        /// </code>
        /// </example>
        [Code("_blank")]
        Blank,

        /// <summary>
        /// Load the response into the HTML 4 frameset parent of the current frame, 
        /// or HTML5 parent browsing context of the current one. If there is no parent, this 
        /// option behaves the same way as _self.
        /// </summary>
        /// <example>
        /// <code language="html" title="Target Parent Example">
        ///     <form target="_parent"></form>
        /// </code>
        /// </example>
        [Code("_parent")]
        Parent,

        /// <summary>
        /// HTML 4: Load the response into the full original window, and cancel all 
        /// other frames. HTML5: Load the response into the top-level browsing context (i.e., 
        /// the browsing context that is an ancestor of the current one, and has no parent). 
        /// If there is no parent, this option behaves the same way as _self.
        /// </summary>
        /// <example>
        /// <code language="html" title="Target Top Example">
        ///     <form target="_top"></form>
        /// </code>
        /// </example>
        [Code("_top")]
        Top,

        /// <summary>
        /// The response is displayed in a named iframe.
        /// </summary>
        /// <example>
        /// <code language="html" title="Target IFrame Example">
        ///     <form target="iframename"></form>
        /// </code>
        /// </example>
        [Code("iframename")]
        IFrame
    }
}