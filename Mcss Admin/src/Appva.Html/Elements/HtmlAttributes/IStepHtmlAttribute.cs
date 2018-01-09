// <copyright file="IStepHtmlAttribute.cs" company="Appva AB">
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
    public interface IStepHtmlAttribute<T> : IHtmlAttribute
    {
        /// <summary>
        /// Works with the min and max attributes to limit the increments at which a numeric 
        /// or date-time value can be set. It can be the string any or a positive floating 
        /// point number. If this attribute is not set to any, the control accepts only 
        /// values at multiples of the step value greater than the minimum.
        /// </summary>
        /// <param name="value">The step.</param>
        /// <returns>The {T}.</returns>
        [Code("step")]
        T Step(string value);
    }
}