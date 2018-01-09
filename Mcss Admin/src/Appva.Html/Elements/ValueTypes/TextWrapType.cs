// <copyright file="TextWrapType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Indicates how the control wraps text.
    /// </summary>
    public enum TextWrapType
    {
        /// <summary>
        /// The browser automatically inserts line breaks (CR+LF) so that each line has no 
        /// more than the width of the control; the cols attribute must be specified.
        /// </summary>
        [Code("hard")]
        Hard = 1,

        /// <summary>
        /// The browser ensures that all line breaks in the value consist of a CR+LF pair, 
        /// but does not insert any additional line breaks.
        /// </summary>
        [Code("soft")]
        Soft = 0
    }
}