// <copyright file="AccountController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Web.Http;
    using Application;
    using Application.Authorization;
    using Core.Extensions;
    using Domain.Repositories;
    using Mcss.Domain.Entities;
    using Models;
    using Repository;
    using Transformers;
    using WebApi.Filters;

    #endregion

    /// <summary>
    /// Account endpoint.
    /// </summary>
    [RoutePrefix("v1/account")]
    public class AccountController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IAccountRepository"/>.
        /// </summary>
        private readonly IAccountRepository accountRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountRepository">The <see cref="IAccountRepository"/></param>
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        
        #endregion

        #region Routes.

        /// <summary>
        /// Returns a hydrated <code>Account</code>.
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns>A hydrated <code>Account</code></returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("{id:guid}")]
        public IHttpActionResult GetById(Guid id)
        {
            var account = this.accountRepository.Get(id);
            if (account.IsNull())
            {
                return this.NotFound();
            }
            return this.Ok(AccountTransformer.ToSingle(account));
        }

        /// <summary>
        /// Search accounts by unique identifier.
        /// </summary>
        /// <param name="query">The string query to search</param>
        /// <returns>A pagaeble set of accounts</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("search")]
        public IHttpActionResult Search([FromUri(Name = "query")] string query = null)
        {
            var searchable = Searchable<Account>.Over(x => x.UniqueIdentifier, query)
                .Page(1).Size(10)
                .OrderBy(x => x.LastName);
            var pageableSet = this.accountRepository.Search(searchable);
            return this.Ok(AccountTransformer.ToPaging(pageableSet));
        }

        /// <summary>
        /// Returns a wrapped <code>Account</code> in a <code>SessionState</code>
        /// </summary>
        /// <param name="credentials">The credentials to authenticate</param>
        /// <returns>An account</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPost, Validate, Route("verify_credentials")]
        public IHttpActionResult VerifyCredentials(AuthenticationCredentialsModel credentials)
        {
            var account = this.accountRepository.VerifyCredentialsByPersonalIdentityNumber(credentials.UserName, credentials.Password);
            if (account.IsNull())
            {
                return this.Unauthorized();
            }
            return this.Ok(AccountTransformer.ToSessionBoundAccount(account));
        }

        /// <summary>
        /// TODO: Checks UID.
        /// </summary>
        /// <param name="uid">The UID</param>
        /// <param name="major">The major</param>
        /// <param name="minor">The minor</param>
        /// <returns>TODO add return statement</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpGet, Route("check_uid")]
        public IHttpActionResult CheckUid([FromUri(Name = "uid")] string uid = null, [FromUri(Name = "major")] string major = null, [FromUri(Name = "minor")] string minor = null)
        {
            var searchable = Searchable<Account>.Over();
            if (uid.IsNotNull())
            {
                if (uid.Length < 2)
                {
                    return this.Ok();
                }
                if (uid.Length > 6 && !uid.Contains("-"))
                {
                    var uniqueIdentifierSize = ((uid.StartsWith("19") || uid.StartsWith("20")) && uid.Length > 8) ? 8 : 6;
                    uid = "{0}-{1}".FormatWith(uid.Substring(0, uniqueIdentifierSize), uid.Substring(uniqueIdentifierSize));
                }
                searchable.MatchBy(x => x.UniqueIdentifier, uid);
            }

            if (major.IsNotNull() && minor.IsNotNull())
            {
                searchable = searchable.FilterBy(x => x.Beacon.Major == major).FilterBy(x => x.Beacon.Minor == minor);   
            }
            var account = this.accountRepository.Search(searchable);
            if (account.TotalCount == 1)
            {
                return this.Ok(AccountTransformer.ToSingle(account.Entities.First()));
            }
            return this.Ok();
        }

        #endregion
    }
}