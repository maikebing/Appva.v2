// <copyright file="WebApiConfig.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring
{
    #region Imports.

    using System.Web.Http;
    using System.Web.Http.Filters;
    using Appva.WebApi.Filters;
    using Appva.WebApi.Formatters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    /// <summary>
    /// Web API configuration.
    /// </summary>
    internal static class WebApiConfig
    {
        /// <summary>
        /// Register HTTP configuration.
        /// </summary>
        /// <param name="config">The <see cref="HttpConfiguration" /></param>
        public static void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { ContractResolver = new LowerCaseUnderScoreContractResolver() };
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            config.Services.Add(typeof(IFilterProvider), new OrderedFilterProvider());
        }
    }
}