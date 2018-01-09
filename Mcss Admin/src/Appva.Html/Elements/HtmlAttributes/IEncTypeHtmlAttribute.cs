// <copyright file="IEncTypeHtmlAttribute.cs" company="Appva AB">
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
    public interface IEncTypeHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// When the value of the method attribute is post, enctype is the MIME type of 
        /// content that is used to submit the form to the server.
        /// <list type="table">
        ///     <listheader>
        ///         <term>value</term>
        ///         <description>description</description>  
        ///     </listheader>
        ///     <item>  
        ///         <term>application/x-www-form-urlencoded</term>
        ///         <description>
        ///         The default value if the attribute is not specified.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>multipart/form-data</term>
        ///         <description>
        ///         Use this value if you are using an input element with the type 
        ///         attribute set to file.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>text/plain</term>
        ///         <description>
        ///         If this attribute is specified, it overrides the enctype attribute of 
        ///         the element's form owner.
        ///         </description>  
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="encoding">The form encoding.</param>
        /// <returns>The {T}.</returns>
        /// <remarks>
        /// <note type="note">
        /// This value can be overridden by a formenctype attribute on a button or input 
        /// element.
        /// </note>
        /// </remarks>
        [Code("enctype")]
        T Encoding(FormEncoding encoding);
    }
}