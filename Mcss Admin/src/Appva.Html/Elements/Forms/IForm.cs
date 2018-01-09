// <copyright file="IForm.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Represents a form element.
    /// </summary>
    public interface IForm<T> : IBlock<T>, /* global */ IAutocompleteHtmlAttribute<T>,
        IAcceptCharsetHtmlAttribute<T>, INoValidateHtmlAttribute<T>, IEncTypeHtmlAttribute<T>,
        INameHtmlAttribute<T>, ITargetHtmlAttribute<T>
    {
        /// <summary>
        /// The fragment identifier (optional last part of a URL separated by a hash mark 
        /// '#') for the form action URI.
        /// </summary>
        /// <param name="id">The identifier without the hash mark.</param>
        /// <returns>The T.</returns>
        [Ignore]
        T Fragment(string id);
    }
}