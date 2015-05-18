// <copyright file="InMemoryHttpServer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Tools
{
    #region Imports.

    using System;
    using System.Web.Http;

    #endregion

    /// <summary>
    /// An in memory HTTP server for testing purposes.
    /// </summary>
    public class InMemoryHttpServer : IDisposable
    {
        #region Variables.

        /// <summary>
        /// The HTTP service.
        /// </summary>
        private readonly HttpServer httpServer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryHttpServer"/> class.
        /// </summary>
        public InMemoryHttpServer()
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            this.httpServer = new HttpServer(config);
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the HTTP Server.
        /// </summary>
        protected HttpServer HttpServer
        {
            get
            {
                return this.httpServer;
            }
        }

        #endregion

        #region IDisposable Members.

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the instance.
        /// </summary>
        /// <param name="disposing">If disposing should be performed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this.httpServer != null)
            {
                this.httpServer.Dispose();
            }
        }

        #endregion
    }
}
