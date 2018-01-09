// <copyright file="IFormMethodHtmlAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    public interface IFormMethodHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// If the input element is a submit button or image, this attribute specifies the 
        /// HTTP method that the browser uses to submit the form. 
        /// Possible values are:
        /// <list type="table">
        ///     <listheader>
        ///         <term>value</term>
        ///         <description>description</description>  
        ///     </listheader>
        ///     <item>  
        ///         <term>post</term>
        ///         <description>
        ///         TThe data from the form is included in the body of the form and is sent 
        ///         to the server.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>get</term>
        ///         <description>
        ///         The data from the form are appended to the form attribute URI, with a 
        ///         '?' as a separator, and the resulting URI is sent to the server. Use this 
        ///         method when the form has no side-effects and contains only ASCII 
        ///         characters.
        ///         </description>  
        ///     </item>
        /// </list>
        /// If specified, this attribute overrides the method attribute of the element's form owner.
        /// </summary>
        /// <param name="method">The form method.</param>
        /// <returns>The {T}.</returns>
        [Code("formmethod")]
        T FormMethod(FormMethod method);
    }
}