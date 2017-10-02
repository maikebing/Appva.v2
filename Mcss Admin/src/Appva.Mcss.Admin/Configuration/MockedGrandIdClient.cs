// <copyright file="MockedGrandIdClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Configuration
{
    #region Imports.

    using Appva.GrandId;
    using Appva.GrandId.Http.Response;
    using Appva.GrandId.Identity;
    using Appva.Mcss.Admin.Application.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MockedGrandIdClient : IGrandIdClient, IMobileGrandIdClient
    {
        #region Fields.

        /// <summary>
        /// The user hsa identifier
        /// </summary>
        private string userHsaId;
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedGrandIdClient"/> class.
        /// </summary>
        public MockedGrandIdClient()
        {
            this.userHsaId = "SE165567766992-132B";
        }

        #endregion

        #region IGrandIdClient members.

        public async Task<FederatedDirectLogin<T>> FederatedDirectLoginAsync<T>(string username, string password) where T : class, IIdentity
        {
            var result = await Task.Run(() => FederatedDirectLogin<T>.CreateNew<T>("dummy-session-id", "user", null));
            return result;
        }

        public async Task<FederatedLogin> FederatedLoginAsync(Uri callbackUri = null)
        {
            var result = await Task.Run(() => FederatedLogin.CreateNew("dummy-session-id", callbackUri));
            return result;
        }

        public async Task<GetSession<T>> GetSessionAsync<T>(string sessionId) where T : class, IIdentity
        {
            var result = await Task.Run(() => GetSession<T>.CreateNew<T>(sessionId, this.userHsaId, null));
            return result;
        }

        public async Task<Logout> LogoutAsync(string sessionId)
        {
            var result = await Task.Run(() => Logout.CreateNew(true));
            return result;
        }

        #endregion
    }
}