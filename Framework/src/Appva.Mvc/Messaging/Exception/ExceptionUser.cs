// <copyright file="ExceptionUser.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc.Messaging
{
    #region Imports.

    using System;
    using System.Security.Claims;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ExceptionUser
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionUser"/> class.
        /// </summary>
        /// <param name="id">The user id</param>
        /// <param name="name">The user name</param>
        private ExceptionUser(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The user ID.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The user name.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ExceptionUser"/> class.
        /// </summary>
        /// <returns>A new instance of <see cref="ExceptionUser"/></returns>
        public static ExceptionUser CreateNew()
        {
            try
            {
                if (HttpContext.Current == null || HttpContext.Current.User == null)
                {
                    return null;
                }
                var principal = HttpContext.Current.User as ClaimsPrincipal;
                if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
                {
                    return null;
                }
                var claimId = principal.FindFirst(ClaimTypes.NameIdentifier);
                var claimName = principal.FindFirst(ClaimTypes.Name);
                var id = claimId == null ? Guid.Empty : new Guid(claimId.Value);
                var name = claimName == null ? string.Empty : claimName.Value;
                return new ExceptionUser(id, name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}