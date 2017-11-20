// <copyright file="TenaConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TenaConfiguration : ValueObject<TenaConfiguration>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaConfiguration"/> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="isInstalled">If the Tena permissions has been installed.</param>
        [JsonConstructor]
        private TenaConfiguration(string clientId, string clientSecret, bool isInstalled) 
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.IsInstalled = isInstalled;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The Tena client id.
        /// </summary>
        [JsonProperty]
        public string ClientId
        {
            get;
            set;
        }

        /// <summary>
        /// The Tena client secret.
        /// </summary>
        [JsonProperty]
        public string ClientSecret
        {
            get;
            set;
        }

        /// <summary>
        /// If the Tena permissions has been installed.
        /// </summary>
        [JsonProperty]
        public bool IsInstalled
        {
            get;
            set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="TenaConfiguration"/> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="isInstalled">If the Tena permissions has been installed.</param>
        /// <returns>A new <see cref="TenaConfiguration"/> instance.</returns>
        public static TenaConfiguration CreateNew(string clientId = null, string clientSecret = null, bool isInstalled = false)
        {
            return new TenaConfiguration(clientId, clientSecret, isInstalled);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ClientId.GetHashCode() +
                   this.ClientSecret.GetHashCode() +
                   this.IsInstalled.GetHashCode();
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.ClientId;
            yield return this.ClientSecret;
            yield return this.IsInstalled;
        }
        #endregion
    }
}