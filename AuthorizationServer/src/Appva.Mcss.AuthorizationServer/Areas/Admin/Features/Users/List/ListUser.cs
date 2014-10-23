// <copyright file="ListUser.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ListUser : Identity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Slug
        {
            get;
            set;
        }

        /// <summary>
        /// A unique identitier for a user.
        /// E.g. for swedish citizens: personnummer.
        /// </summary>
        public string PersonalIdentityNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The users' first name.
        /// E.g. "John".
        /// </summary>
        public string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// The users' last name.
        /// E.g. "Doe".
        /// </summary>
        public string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FullName
        {
            get
            {
                return string.Join(" ", (this.FirstName + " " + this.LastName).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }
    }
}