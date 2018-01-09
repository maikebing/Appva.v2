// <copyright file="IFormTargetHtmlAttribute.cs" company="Appva AB">
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
    public interface IFormTargetHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the input element is a submit button or image, this attribute is a name or 
        /// keyword indicating where to display the response that is received after 
        /// submitting the form. This is a name of, or keyword for, a browsing context (e.g. 
        /// tab, window, or inline frame). If this attribute is specified, it overrides the 
        /// target attribute of the elements's form owner. The following keywords have 
        /// special meanings:
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
        [Code("formtarget")]
        T FormTarget(FormTarget target);
    }
}