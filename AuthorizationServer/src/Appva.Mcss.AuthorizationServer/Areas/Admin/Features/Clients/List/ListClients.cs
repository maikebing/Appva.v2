// <copyright file="ListClients.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListClients : IdentityAndSlug
    {
        /// <summary>
        /// The client name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The client description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The client avatar.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}