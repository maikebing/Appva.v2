// <copyright file="UserService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cryptography;
    using Appva.Mcss.AuthorizationServer.Domain.Authentication;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UserService : IUserService
    {
        #region Variables.

        /// <summary>
        /// 
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="persistenceContext"></param>
        public UserService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IUserService Members

        /// <inheritdocs />
        public bool AuthenticateWithPersonalIdentityNumber(string personalIdentityNumber, string password, string authenticationMethod, out User user)
        {
            // Validate personalIdentityNumber
            // Validate password to policy
            user = this.UserByPersonalIdentityNumber(personalIdentityNumber);
            if (user == null)
            {
                return false;
            }
            var authentication = this.UserAuthentication(user, authenticationMethod);
            if (authentication == null)
            {
                return false;
            }
            if (this.HasTooManyRecentPasswordFailures(authentication))
            {
                return false;
            }
            var auth = (PersonalIdentityNumberPasswordAuthentication) JsonConvert.DeserializeObject(authentication.Authentication, typeof(PersonalIdentityNumberPasswordAuthentication));
            if (Password.NotEquals(password, auth.Password))
            {
                this.RecordFailedLogin(authentication);
                return false;
            }
            /*if (user.IsNotActive() || user.IsNotVerified() || user.IsBlocked())
            {
                return false;
            }*/
            return true;
        }

        /// <inheritdocs />
        public User UserByPersonalIdentityNumber(string personalIdentityNumber)
        {
            return this.persistenceContext.QueryOver<User>()
                .Where(x => x.PersonalIdentityNumber == personalIdentityNumber)
                .SingleOrDefault();
        }

        /// <inheritdocs />
        public UserAuthentication UserAuthentication(User user, string authenticationMethod)
        {
            return this.persistenceContext.QueryOver<UserAuthentication>()
                .Where(x => x.User.Id == user.Id)
                .Left.JoinQueryOver<UserAuthenticationMethod>(x => x.UserAuthenticationMethod)
                .Where(x => x.Key == authenticationMethod)
                .SingleOrDefault();
        }

        /// <inheritdocs />
        public bool IsPasswordExpired(User user)
        {
            // TODO: check PasswordResetFrequency PasswordChanged
            return false;
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authentication"></param>
        /// <returns></returns>
        private bool HasTooManyRecentPasswordFailures(UserAuthentication authentication)
        {
            // TODO: Create method HasTooManyRecentPasswordFailures(UserAuthentication authentication))
            /*
            var result = false;
            if (Configuration.UserLockoutFailedLoginAttempts <= authentication.FailedLoginCount)
            {
                result = authentication.LastFailedLogin >= UtcNow.Subtract(Configuration.AccountLockoutDuration);
                if (! result)
                {
                    authentication.FailedLoginCount = 0;
                }
            }
            if (result)
            {
                authentication.FailedLoginCount++;
            }
            return result;
            */
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authentication"></param>
        private void RecordFailedLogin(UserAuthentication authentication)
        {
            // TODO: Create method RecordFailedLogin(UserAuthentication authentication)
            // Add to FailedLoginCount + 1
            // Update LastFailedLogin
        }

        #endregion
    }
}