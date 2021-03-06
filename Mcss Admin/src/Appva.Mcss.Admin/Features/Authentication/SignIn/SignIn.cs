﻿// <copyright file="SignIn.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SignIn : IRequest<SignInForm>
    {
        /// <summary>
        /// The return URL.
        /// </summary>
        public string ReturnUrl
        {
            get;
            set;
        }
    }
}