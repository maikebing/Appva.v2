// <copyright file="SithsClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Siths
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Appva.Siths.Configuration;
    using Appva.Siths.Identity;
    using Appva.Siths.Security;

    #endregion

    /// <summary>
    /// Authify SITHS client.
    /// </summary>
    public interface ISithsClient
    {
        /// <summary>
        /// Returns the identity provider external SITHS <c>Uri</c>.
        /// </summary>
        /// <returns>A <see cref="Task{Uri}"/></returns>
        Task<Uri> ExternalLoginUri();

        /// <summary>
        /// Returns the siths identity.
        /// </summary>
        /// <param name="token">The authentication token</param>
        /// <returns>A <see cref="Task{SithsIdentity}"/></returns>
        Task<SithsIdentity> Identity(string token);

        /// <summary>
        /// Logs out the user account from the identity provider (IdP).
        /// </summary>
        /// <param name="token">The authentication token</param>
        /// <returns>A <see cref="Task{bool}"/>; true if successful</returns>
        Task<string> Logout(string token);
    }

    /// <summary>
    /// The SITHS authentication client.
    /// <example>
    /// The preferred usage is to use the client as a singleton and not dispose after 
    /// each call. 
    /// <code language="cs" title="Not Preferred Example">
    ///     using (var client = new SithsClient(...))
    ///     {
    ///         await client.ExternalLoginUri();
    ///     }
    /// </code>
    /// <code language="cs" title="Preferred Example with IoC">
    ///     var builder = new ContainerBuilder();
    ///     builder.RegisterType{SithsClient}().As{ISithsClient}().SingleInstance();
    /// </code>
    /// </example>
    /// </summary>
    public sealed class SithsClient : AuthifyClient, ISithsClient
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SithsClient"/> class.
        /// </summary>
        /// <param name="tokenizer">The token provider to use</param>
        /// <param name="configuration">The siths configuration</param>
        public SithsClient(ITokenizer tokenizer, ISithsConfiguration configuration = null)
            : base(tokenizer, configuration ?? (IAuthifyConfiguration) ConfigurationManager.GetSection("siths"))
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="SithsClient"/> class.
        /// </summary>
        /// <param name="tokenizer">The token provider to use</param>
        /// <param name="configuration">The siths configuration</param>
        /// <returns>A new <see cref="ISithsClient"/> instance</returns>
        public static ISithsClient CreateNew(ITokenizer tokenizer, ISithsConfiguration configuration = null)
        {
            return new SithsClient(tokenizer, configuration);
        }

        #endregion

        #region ISithsClient Members.

        /// <inheritdoc />
        async Task<Uri> ISithsClient.ExternalLoginUri()
        {
            return await this.RequireLogin();
        }

        /// <inheritdoc />
        async Task<SithsIdentity> ISithsClient.Identity(string token)
        {
            var result = await this.GetResponse<SithsIdentity>(token);
            if (result == null || result.Identities.Count != 1)
            {
                return null;
            }
            return result.Identities.First();
        }

        /// <inheritdoc />
        async Task<string> ISithsClient.Logout(string token)
        {
            return await this.RequireLogout(token);
        }

        #endregion
    }
}