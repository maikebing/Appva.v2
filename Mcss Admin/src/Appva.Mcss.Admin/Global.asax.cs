// <copyright file="Global.asax.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Security.Claims;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Appva.Mvc.Engines;
    using log4net.Config;

    #endregion

    /// <summary>
    /// Global application configuration setup.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <inheritdoc />
        protected void Application_Start()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            XmlConfigurator.Configure();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            IocConfiguration.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfiguration.RegisterRoutes(RouteTable.Routes);
            BundleConfiguration.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }
    }
}