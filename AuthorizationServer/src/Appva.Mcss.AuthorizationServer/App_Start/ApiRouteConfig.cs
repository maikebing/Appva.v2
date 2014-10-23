// <copyright file="ApiRouteConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer
{
    #region Imports.

    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Appva.WebApi.Formatters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    /// <summary>
    /// Register API routes.
    /// </summary>
    public class ApiRouteConfig
    {
        /// <summary>
        /// Registers MVC routes.
        /// </summary>
        /// <param name="routes">Collection of routes</param>
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new LowerCaseUnderScoreContractResolver(),
                MissingMemberHandling = MissingMemberHandling.Error
            };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
        }
    }
}
