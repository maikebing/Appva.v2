// <copyright file="UserAuthentication.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using Authentication;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// Managable user authentication credentials.
    /// </summary>
    public class UserAuthentication : Entity<UserAuthentication>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthentication"/> class.
        /// </summary>
        public UserAuthentication(User user, IAuthentication authentication, UserAuthenticationMethod userAuthenticationMethod)
        {
            // TODO: Requires.NotNull(user, "user");
            // TODO: Requires.NotNull(authentication, "authentication");
            // TODO: Requires.NotNull(userAuthenticationMethod, "userAuthenticationMethod");
            this.User = user;
            this.Authentication = JsonConvert.SerializeObject(authentication);
            this.UserAuthenticationMethod = userAuthenticationMethod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthentication"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected UserAuthentication()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The user.
        /// </summary>
        public virtual User User
        {
            get;
            protected set;
        }

        /// <summary>
        /// The user authentication.
        /// </summary>
        public virtual string Authentication
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' authentication method.
        /// </summary>
        public virtual UserAuthenticationMethod UserAuthenticationMethod
        {
            get;
            protected set;
        }

        #endregion
    }
}