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
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
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
            NHibernateProfiler.Initialize();
            XmlConfigurator.Configure();
            ModelBinders.Binders.DefaultBinder = new AdminModelBinder();
            //ValueProviderFactories.Factories.Add();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AreaRegistration.RegisterAllAreas();
            FilterConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfiguration.RegisterRoutes(RouteTable.Routes);
            BundleConfiguration.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }
    }
}