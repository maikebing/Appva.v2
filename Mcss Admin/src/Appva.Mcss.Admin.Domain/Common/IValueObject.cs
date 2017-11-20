// <copyright file="IValueObject.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// An object that contains attributes but has no conceptual identity. They should 
    /// be treated as immutable.
    /// </summary>
    public interface IValueObject
    {
    }
}