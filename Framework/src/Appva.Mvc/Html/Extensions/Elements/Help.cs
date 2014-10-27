// <copyright file="Help.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mvc.Html.Extensions.Elements
{
    #region Imports.

    using System.Web.Mvc;
    using Mvc.Html.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Help<TRoot, TModel, TProperty> : Element<TRoot>
        where TRoot : ElementBuilder<TModel>, IFormGroupFor<TModel, TProperty>
    {
        #region Variables.

        /// <summary>
        /// The help text.
        /// </summary>
        private readonly string helpText;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Help{TRoot, TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        /// <param name="helpText">The help text</param>
        internal Help(TRoot root, string helpText)
            : base(root)
        {
            this.helpText = helpText;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public override MvcHtmlString Build()
        {
            return this.Root.HtmlHelper.Help(this.helpText);
        }

        #endregion
    }
}