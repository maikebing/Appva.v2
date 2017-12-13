// <copyright file="Permission.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Security
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Contracts.Permissions;
    using Appva.Core.Resources;

    #endregion

    /// <summary>
    /// An implementation of <see cref="IPermission"/>.
    /// </summary>
    internal sealed class Permission : IPermission/*, KeyValuePair<string, string>*/
    {
        #region Variables.

        /// <summary>
        /// The permission key.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The permission value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="value">The permission value.</param>
        public Permission(string value)
        {
            this.key   = ClaimTypes.Permission;
            this.value = value;
        }

        #endregion

        #region IPermission Members.

        /// <inheritdoc />
        public string Key
        {
            get
            {
                return this.key;
            }
        }

        /// <inheritdoc />
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="value">The permission value.</param>
        /// <returns>A new <see cref="Permission"/> instance.</returns>
        public static Permission New(string value)
        {
            return new Permission(value);
        }

        #endregion
    }
}