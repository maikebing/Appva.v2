// <copyright file="ListTenaModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListTenaModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The client id.
        /// </summary>
        public string ClientId
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string ClientSecret
        {
            get;
            set;
        }

        /// <summary>
        /// The masked client id.
        /// </summary>
        public string ClientIdMask
        {
            get;
            set;
        }

        /// <summary>
        /// The masked client secret.
        /// </summary>
        public string ClientSecretMask
        {
            get;
            set;
        }

        /// <summary>
        /// If the Tena function has been installed.
        /// </summary>
        public bool IsInstalled
        {
            get;
            set;
        }

        #endregion
    }
}