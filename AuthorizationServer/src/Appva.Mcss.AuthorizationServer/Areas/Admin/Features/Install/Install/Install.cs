// <copyright file="Install.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Install : IRequest<Install>
    {
        /// <summary>
        /// Whether installed or not.
        /// </summary>
        public bool IsInstalled
        {
            get;
            set;
        }

        /// <summary>
        /// Whether install was successful or not.
        /// </summary>
        public bool IsException
        { 
            get; 
            set; 
        }

        /// <summary>
        /// An exception message if any.
        /// </summary>
        public string ExceptionMessage
        {
            get;
            set;
        }
    }
}