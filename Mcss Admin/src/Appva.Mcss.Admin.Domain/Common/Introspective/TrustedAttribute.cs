// <copyright file="TrustedAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Common
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Indicates that a parameter is trusted to be valid, so callers must take care
    /// to only pass valid values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute should never be applied to parameters in public members, as all 
    /// public members should validate their parameters. The exception here is public 
    /// members within internal types, as those aren't really exposed publicly.
    /// </para>
    /// <para>
    /// Parameters decorated with this attribute should typically be validated in debug 
    /// configurations, using <see cref="Debug.Assert(bool, string)"/> or a similar 
    /// method.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class TrustedAttribute : Attribute
    {
    }
}