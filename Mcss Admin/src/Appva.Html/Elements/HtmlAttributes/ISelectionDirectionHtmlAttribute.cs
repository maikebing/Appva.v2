// <copyright file="ISelectionDirectionHtmlAttribute.cs" company="Appva AB">
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
    public interface ISelectionDirectionHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// The direction in which selection occurred. This is "forward" if the selection 
        /// was made from left-to-right in an LTR locale or right-to-left in an RTL locale, 
        /// or "backward" if the selection was made in the opposite direction. On platforms 
        /// on which it's possible this value isn't known, the value can be "none"; for 
        /// example, on macOS, the default direction is "none", then as the user begins to 
        /// modify the selection using the keyboard, this will change to reflect the 
        /// direction in which the selection is expanding.
        /// <list type="table">
        ///     <listheader>
        ///         <term>value</term>
        ///         <description>description</description>  
        ///     </listheader>
        ///     <item>  
        ///         <term>forward</term>
        ///         <description>
        ///         If selection was performed in the start-to-end direction of the current 
        ///         locale.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>backward</term>
        ///         <description>
        ///         If selection was performed in the start-to-end direction of the current 
        ///         locale for the opposite direction.
        ///         </description>  
        ///     </item>
        ///     <item>  
        ///         <term>none</term>
        ///         <description>
        ///         If the direction is unknown.
        ///         </description>  
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="direction">
        /// The direction, either "forward", "backward" or "none".
        /// </param>
        /// <returns>The {T}.</returns>
        [Code("selectionDirection")]
        T SelectionDirection(string direction);
    }
}