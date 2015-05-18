// <copyright file="FeatureViewLocationRazorViewEngine.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Feature Folders in ASP.NET MVC which will use a new folder structure:
    /// Features/User/Index/Index.chtml
    /// Features/User/Index/UserModel.cs
    /// Features/User/Index/UserIdQuery.cs
    /// </summary>
    /// <example>
    /// ViewEngines.Engines.Clear();
    /// ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
    /// </example>
    public sealed class FeatureViewLocationRazorViewEngine : RazorViewEngine
    {
        #region Variables.

        /// <summary>
        /// The view formats.
        /// </summary>
        private readonly string[] formats =
        {
            "~/Features/{1}/{0}.cshtml",
            "~/Features/{1}/{0}/{0}.cshtml",
            "~/Features/{1}/Partials/{0}.cshtml",
            "~/Features/{1}/Partials/{0}/{0}.cshtml",
            "~/Features/Shared/{0}.cshtml"
        };

        /// <summary>
        /// The area view formats.
        /// </summary>
        private readonly string[] areaFormats =
        {
            "~/Areas/{2}/Features/{1}/{0}.cshtml",
            "~/Areas/{2}/Features/{1}/{0}/{0}.cshtml",
            "~/Areas/{2}/Features/{1}/Partials/{0}.cshtml",
            "~/Areas/{2}/Features/{1}/Partials/{0}/{0}.cshtml",
            "~/Areas/{2}/Features/Shared/{0}.cshtml"
        };

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureViewLocationRazorViewEngine"/> class.
        /// </summary>
        public FeatureViewLocationRazorViewEngine()
        {
            this.ViewLocationFormats = this.ViewLocationFormats.Union(this.formats).ToArray();
            this.MasterLocationFormats = this.MasterLocationFormats.Union(this.formats).ToArray();
            this.PartialViewLocationFormats = this.PartialViewLocationFormats.Union(this.formats).ToArray();
            this.AreaViewLocationFormats = this.AreaViewLocationFormats.Union(this.areaFormats).ToArray();
            this.AreaMasterLocationFormats = this.AreaMasterLocationFormats.Union(this.areaFormats).ToArray();
            this.AreaPartialViewLocationFormats = this.AreaPartialViewLocationFormats.Union(this.areaFormats).ToArray();
        }

        #endregion
    }
}