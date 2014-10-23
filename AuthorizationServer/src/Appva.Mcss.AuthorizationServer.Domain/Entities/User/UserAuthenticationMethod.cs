// <copyright file="UserAuthenticationMethod.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// User authentication methods.
    /// </summary>
    public class UserAuthenticationMethod : Entity<UserAuthenticationMethod>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationMethod"/> class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="service"></param>
        public UserAuthenticationMethod(string key, string name, string description, string service, string formView)
        {
            // TODO: Requires.NotNullOrEmpty(key, "key");
            this.Key = key;
            this.Name = name;
            this.Description = description;
            this.Service = service;
            this.FormView = formView;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAuthenticationMethod"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected UserAuthenticationMethod()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The authentication method key.
        /// E.g. "siths", "web_pwd", "client_pwd".
        /// </summary>
        public virtual string Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authentication method name.
        /// E.g. "SITHS credentials".
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authentication method description.
        /// E.g. "Credentials used for SITHS sign in".
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The authentication service.
        /// </summary>
        public virtual string Service
        {
            get;
            protected set;
        }

        /// <summary>
        /// The internal MVC form view.
        /// </summary>
        public virtual string FormView
        {
            get;
            protected set;
        }

        #endregion
    }
}