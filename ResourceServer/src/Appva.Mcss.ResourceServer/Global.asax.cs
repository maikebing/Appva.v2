// <copyright file="Global.asax.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer
{
    #region Imports.

    using System.Web;
    using System.Web.Http;
    using HibernatingRhinos.Profiler.Appender.NHibernate;

    #endregion

    /// <summary>
    /// Web Api bootstrapping. 
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <inheritdoc />
        protected void Application_Start()
        {
            //NHibernateProfiler.Initialize();
            AutoFacConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Configure);
        }
    }
}