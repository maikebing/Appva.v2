// <copyright file="ITargetHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// A name or keyword indicating where to display the response that is received 
    /// after submitting the form.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface ITargetHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// A name or keyword indicating where to display the response that is received 
        /// after submitting the form. In HTML 4, this is the name/keyword for a frame. 
        /// In HTML5, it is a name/keyword for a browsing context (for example, tab, window, 
        /// or inline frame).
        /// <list type="table">
        ///     <listheader>
        ///         <term>value</term>
        ///         <description>description</description>  
        ///     </listheader>
        ///     <item>  
        ///         <term>_self</term>
        ///         <description>
        ///         Load the response into the same browsing context as the current one. 
        ///         This value is the default if the attribute is not specified.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>_blank</term>
        ///         <description>
        ///         Load the response into a new unnamed browsing context.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>_parent</term>
        ///         <description>
        ///         Load the response into the parent browsing context of the current one. 
        ///         If there is no parent, this option behaves the same way as _self.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>_top</term>
        ///         <description>
        ///         Load the response into the top-level browsing context (i.e. the browsing 
        ///         context that is an ancestor of the current one, and has no parent). If 
        ///         there is no parent, this option behaves the same way as _self.
        ///         </description>  
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="target">The form target.</param>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// <note type="note">
        /// This value can be overridden by a formtarget attribute on a button or 
        /// input element.
        /// </note>
        /// </remarks>
        [Code("target")]
        T Target(FormTarget target);
    }
}