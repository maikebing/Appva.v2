// <copyright file="Resource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Resource : AggregateRoot<Client>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="tokenLifetime"></param>
        /// <param name="publicAccessTokenEncryptionKey"></param>
        /// <param name="scopes"></param>
        public Resource(string name, string description, int tokenLifetime, string publicAccessTokenEncryptionKey, IList<Scope> scopes)
        {
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Slug = new Slug(name);
            this.Name = name;
            this.Description = description;
            this.TokenLifetime = tokenLifetime;
            this.PublicAccessTokenEncryptionKey = publicAccessTokenEncryptionKey;
            this.Scopes = scopes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Resource()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether the resource is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The client slug.
        /// </summary>
        public virtual Slug Slug
        {
            get;
            protected set;
        }

        /// <summary>
        /// The resource name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// The resource description.
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// The resource token lifetime.
        /// </summary>
        public virtual int TokenLifetime
        {
            get;
            protected set;
        }

        /// <summary>
        /// The public access token encryption key.
        /// TODO: Replace this with Signing keys from certificates or other
        /// formats. 
        /// </summary>
        public virtual string PublicAccessTokenEncryptionKey
        {
            get;
            protected set;
        }

        /// <summary>
        /// The scopes.
        /// </summary>
        public virtual IList<Scope> Scopes
        {
            get;
            protected set;
        }

        public virtual HashSet<string> SupportedScopes
        {
            get
            {
                return new HashSet<string>(this.Scopes.Select(x => x.Key).ToList());
            }
        }

        public virtual RSACryptoServiceProvider EncryptionProvider
        {
            get;
            protected set;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual RSACryptoServiceProvider PublicTokenEncrypter
        {
            get
            {
                if (this.EncryptionProvider.IsNull())
                {
                    lock (this)
                    {
                        this.EncryptionProvider = new RSACryptoServiceProvider();
                        this.EncryptionProvider.FromXmlString(this.PublicAccessTokenEncryptionKey.FromBase64().ToUtf8());
                    }
                }
                return this.EncryptionProvider;
            }
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Activates the resource.
        /// </summary>
        public virtual void Activate()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Inactivates the resource.
        /// </summary>
        public virtual void Inactivate()
        {
            this.IsActive = false;
        }

        #endregion
    }
}