// <copyright file="ValidateTenantIdentificationResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Identity
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// Represents a result from a tenant identification validation.
    /// </summary>
    public interface IValidateTenantIdentificationResult
    {
        /// <summary>
        /// Returns whether or not the tenant identification provided is valid.
        /// </summary>
        bool IsValid
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the tenant identification provided is invalid.
        /// </summary>
        bool IsInvalid
        {
            get;
        }

        /// <summary>
        /// Returns whether or not a tenant identification was found.
        /// </summary>
        bool IsNotFound
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ValidateTenantIdentificationResult : IValidateTenantIdentificationResult
    {
        #region Variables.

        /// <summary>
        /// The tenant identification is valid.
        /// </summary>
        public static readonly IValidateTenantIdentificationResult Valid = ValidateTenantIdentificationResult.CreateNew(ValidateTenantIdentificationResultCode.Valid);

        /// <summary>
        /// The tenant identification is invalid.
        /// </summary>
        public static readonly IValidateTenantIdentificationResult Invalid = ValidateTenantIdentificationResult.CreateNew(ValidateTenantIdentificationResultCode.Invalid);

        /// <summary>
        /// The tenant identification is not found.
        /// </summary>
        public static readonly IValidateTenantIdentificationResult NotFound = ValidateTenantIdentificationResult.CreateNew(ValidateTenantIdentificationResultCode.NotFound);

        /// <summary>
        /// The <see cref="ValidateTenantIdentificationResultCode"/>.
        /// </summary>
        private readonly ValidateTenantIdentificationResultCode code;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateTenantIdentificationResult"/> class.
        /// </summary>
        /// <param name="code">The result code</param>
        private ValidateTenantIdentificationResult(ValidateTenantIdentificationResultCode code)
        {
            this.code = code;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ValidateTenantIdentificationResult"/> class.
        /// </summary>
        /// <param name="code">The result code</param>
        /// <returns>A new <see cref="ValidateTenantIdentificationResult"/> instance</returns>
        public static ValidateTenantIdentificationResult CreateNew(ValidateTenantIdentificationResultCode code)
        {
            return new ValidateTenantIdentificationResult(code);
        }

        #endregion

        #region Internal Enums.

        /// <summary>
        /// The tenant authorization result code.
        /// </summary>
        public enum ValidateTenantIdentificationResultCode : int
        {
            /// <summary>
            /// Valid tenant identification.
            /// </summary>
            Valid = 0,

            /// <summary>
            /// Invalid tenant identification; e.g. not valid for the current URL, 
            /// invalid due to expired certificate etc.
            /// </summary>
            Invalid = 1,

            /// <summary>
            /// No tenant identification found.
            /// </summary>
            NotFound = 2,
        }

        #endregion

        #region IValidateTenantIdentificationResult Members.

        /// <inheritdoc />
        public bool IsValid
        {
            get
            {
                return this.code.Equals(ValidateTenantIdentificationResultCode.Valid);
            }
        }

        /// <inheritdoc />
        public bool IsInvalid
        {
            get
            {
                return this.code.Equals(ValidateTenantIdentificationResultCode.Invalid);
            }
        }

        /// <inheritdoc />
        public bool IsNotFound
        {
            get
            {
                return this.code.Equals(ValidateTenantIdentificationResultCode.NotFound);
            }
        }

        #endregion
    }
}