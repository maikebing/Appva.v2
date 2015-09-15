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
    using Appva.Core.Logging;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mvc;
    using Appva.Mvc.Configuration;
    using HibernatingRhinos.Profiler.Appender.NHibernate;
    using log4net.Config;
    using Appva.Mvc.ModelBinding;

    #endregion

    /// <summary>
    /// Global application configuration setup.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<MvcApplication>();

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcApplication"/> class.
        /// </summary>
        public MvcApplication()
        {
            XmlConfigurator.Configure();
        }

        #endregion
    
        #region HttpApplication Overrides.

        /// <inheritdoc />
        protected void Application_Start()
        {
            Log.Info("Application started ...");
            MvcHandler.DisableMvcResponseHeader = true;
            ModelBinders.Binders.DefaultBinder = new AdminModelBinder();
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AreaRegistration.RegisterAllAreas();
            FilterConfiguration.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfiguration.RegisterRoutes(RouteTable.Routes);
            BundleConfiguration.RegisterBundles(BundleTable.Bundles);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }

        /// <inheritdoc />
        protected void Application_End()
        {
            Log.Info("Application ended ...");
        }

        #endregion
    }
}