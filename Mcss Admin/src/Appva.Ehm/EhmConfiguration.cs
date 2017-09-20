// <copyright file="EhmConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EhmConfiguration
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmConfiguration"/> class.
        /// </summary>
        public EhmConfiguration(string baseUrl)
            : this(new Uri(baseUrl))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EhmConfiguration"/> class.
        /// </summary>
        public EhmConfiguration(Uri baseUrl)
        {
            this.baseUri = baseUrl;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The base url
        /// </summary>
        internal Uri baseUri
        {
            get;
            private set;
        }

        #endregion

        #region Endpoints.

        /// <summary>
        /// The endpoints in the api
        /// </summary>
        internal class Endpoints
        {
            #region Const.

            /// <summary>
            /// The list endpoint
            /// </summary>
            internal const string List = "/api/v1/ordinationer";

            #endregion
        }
        

        #endregion
    }
}