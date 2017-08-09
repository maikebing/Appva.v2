// <copyright file="TenaService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>


namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// The <see cref="ITenaService"/>.
    /// </summary>
    public interface ITenaService : IService
    {
        /// <summary>
        /// Encode credentials to base64.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <param name="secret">The client secret.</param>
        /// <returns></returns>
        string Base64Encode(string clientId, string clientSecret);
    }

    /// <summary>
    /// The <see cref="TenaService"/> service.
    /// </summary>
    public sealed class TenaService : ITenaService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ITenaRepository"/>.
        /// </summary>
        private readonly ITenaRepository repository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenaService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="ITenaRepository"/>.</param>
        public TenaService(ITenaRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region ITenaService members.

        /// <inheritdoc />
        public string Base64Encode(string clientId, string clientSecret)
        {
            var credentials = System.Text.Encoding.UTF8.GetBytes(clientId + ":" + clientSecret);
            return Convert.ToBase64String(credentials);
        }
        
        #endregion
    }
}
