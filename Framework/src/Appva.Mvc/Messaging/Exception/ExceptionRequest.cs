// <copyright file="ExceptionRequest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ExceptionRequest
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionRequest"/> class.
        /// </summary>
        /// <param name="data">The request headers, request URL, request type etc</param>
        public ExceptionRequest(IDictionary<string, string> data)
        {
            this.Data = data;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The request headers.
        /// </summary>
        public IDictionary<string, string> Data
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ExceptionRequest"/> class.
        /// </summary>
        /// <returns>A new instance of <see cref="ExceptionRequest"/></returns>
        public static ExceptionRequest CreateNew()
        {
            try
            {
                if (HttpContext.Current == null || HttpContext.Current.Request == null)
                {
                    return null;
                }
                var data = new Dictionary<string, string>();
                data.Add("REQUEST URL", HttpContext.Current.Request.RawUrl);
                data.Add("REQUEST TYPE", HttpContext.Current.Request.RequestType);
                foreach (var key in HttpContext.Current.Request.Headers.AllKeys)
                {
                    //// Filter out potiential private and secure data.
                    if (!key.ToLower().Equals("cookie") &&
                        !key.ToLower().Equals("client-serialnumber") &&
                        !key.ToLower().Equals("ns-appflow-id"))
                    {
                        data.Add(key.ToUpper(), HttpContext.Current.Request.Headers.Get(key));
                    }
                }
                return new ExceptionRequest(data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}