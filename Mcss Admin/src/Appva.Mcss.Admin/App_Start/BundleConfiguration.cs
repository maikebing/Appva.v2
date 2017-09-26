// <copyright file="BundleConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Web.Optimization;

    #endregion

    /// <summary>
    /// The bundling configuration.
    /// </summary>
    internal static class BundleConfiguration
    {
        /// <summary>
        /// Register all CSS and JavaScript bundles.
        /// </summary>
        /// <param name="bundles">The <see cref="BundleCollection"/></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/Assets/js/bundle").Include(
                "~/Assets/js/jquery/jquery-1.6.3.js", "~/Assets/js/jquery/jquery-ui-{version}.custom.js",
                "~/Assets/js/jquery/plugins/date.js", "~/Assets/js/jquery/plugins/jquery.datepicker.js",
                "~/Assets/js/jquery/plugins/excanvas.js", "~/Assets/js/jquery/plugins/jquery.flot-{version}.js",
                "~/Assets/js/jquery/plugins/jquery.validate-{version}.js"
            ));
            bundles.Add(new ScriptBundle("~/Assets/js/mcss/bundle").Include(
                "~/Assets/js/main.js", "~/Assets/js/mcss.js", "~/Assets/js/mcss.customselect.js", "~/Assets/js/mcss.chart.js",
                    "~/Assets/js/mcss.validation.js", "~/Assets/js/mcss.calendar.js", "~/Assets/js/mcss.calendar.lb.js", "~/Assets/js/mcss.lightbox.js", "~/Assets/js/mcss.prepareweeks.js", "~/Assets/js/mcss.order.js", "~/Assets/js/mcss.domready.js"
            ));
            bundles.Add(new StyleBundle("~/Assets/css/ui-lightness/bundle").Include(
                "~/Assets/css/ui-lightness/jquery-ui-{version}.custom.css"
            ));
            bundles.Add(new StyleBundle("~/Assets/css/bundle").Include(
                "~/Assets/css/datePicker.css", "~/Assets/css/main.css", "~/Assets/css/ehm.css"
            ));
            //// Authorization and external user management
            bundles.Add(new StyleBundle("~/Assets/css/auth").Include(
                "~/Assets/css/auth.css"
            ));
            bundles.Add(new ScriptBundle("~/Assets/js/signin").Include(
                "~/Assets/js/jquery/jquery-1.6.3.js"
            ));
            bundles.Add(new ScriptBundle("~/Assets/js/auth").Include(
                "~/Assets/js/jquery/jquery-1.6.3.js", "~/Assets/js/zxcvbn.js", "~/Assets/js/mcss.password-strength.js"
            ));
        }
    }
}