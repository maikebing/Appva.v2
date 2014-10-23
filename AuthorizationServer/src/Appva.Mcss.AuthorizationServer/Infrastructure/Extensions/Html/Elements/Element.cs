// <copyright file="Element.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure.Extensions.Html.Elements
{
    #region Imports.

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IElement
    {
        MvcHtmlString Build();
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class Element<TRoot> : IElement
    {
        #region Variables.

        /// <summary>
        /// The root.
        /// </summary>
        private readonly TRoot root;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Element{TRoot}"/> class.
        /// </summary>
        /// <param name="root">The root</param>
        protected Element(TRoot root)
        {
            this.root = root;
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the element builder.
        /// </summary>
        protected TRoot Root
        {
            get
            {
                return this.root;
            }
        }


        #endregion

        #region Abstract Methods.

        /// <inheritdoc />
        public abstract MvcHtmlString Build();

        #endregion
    }
}