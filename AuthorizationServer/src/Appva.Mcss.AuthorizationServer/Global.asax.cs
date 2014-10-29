// <copyright file="Global.asax.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System.IdentityModel.Claims;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.Mcss.AuthorizationServer.Code;
    using Appva.Mvc.Engines;
    using HibernatingRhinos.Profiler.Appender.NHibernate;


    #endregion

    /// <summary>
    /// Global application configuration setup.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <inheritdoc />
        protected void Application_Start()
        {
            //// FIXME: Remove web api!
            //// FIXME: NHibernateProfiler nuget!
            //// NHibernateProfiler.Initialize();
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(ApiRouteConfig.RegisterRoutes);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());
        }
    }
}
