// <copyright file="AuthenticationResult.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// Represents a result of a processed authentication attempt.
    /// </summary>
    public interface IAuthenticationResult
    {
        /// <summary>
        /// Whether or not the authentication was success or not.
        /// </summary>
        bool IsAuthorized
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to a general
        /// failure, e.g. roles and permissions.
        /// </summary>
        bool IsGeneralFailure
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to invalid 
        /// credential being supplied.
        /// </summary>
        bool IsFailureDueToInvalidCredentials
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to identity 
        /// being ambiguous.
        /// </summary>
        bool IsFailureDueToAmbigiousIdentity
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to identity 
        /// not being found.
        /// </summary>
        bool IsFailureDueToIdentityNotFound
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to 
        /// uncategorized reasons.
        /// </summary>
        bool IsFailureDueToUncategorizedReason
        {
            get;
        }

        /// <summary>
        /// Returns whether or not the result is an unauthorized failure due to identity 
        /// being locked out.
        /// </summary>
        bool IsFailureDueToIdentityLockout
        {
            get;
        }

        /// <summary>
        /// Returns the authentication result code.
        /// </summary>
        AuthenticationResult.ResultCode Code
        {
            get;
        }

        /// <summary>
        /// Returns the authenticated identity. If the authentication result is unsuccessful 
        /// the identity will be null;
        /// </summary>
        Account Identity
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthenticationResult : IAuthenticationResult
    {
        #region Variables.

        /// <summary>
        /// Generic failure result.
        /// </summary>
        public static readonly IAuthenticationResult Failure = new AuthenticationResult(ResultCode.Failure);

        /// <summary>
        /// Failure due to invalid credentials result.
        /// </summary>
        public static readonly IAuthenticationResult InvalidCredentials = new AuthenticationResult(ResultCode.InvalidCredentials);

        /// <summary>
        /// Failure due to ambigious identity.
        /// </summary>
        public static readonly IAuthenticationResult AmbigiousIdentity = new AuthenticationResult(ResultCode.AmbigiousIdentity);

        /// <summary>
        /// Failiure due to identity not found.
        /// </summary>
        public static readonly IAuthenticationResult NotFound = new AuthenticationResult(ResultCode.NotFound);

        /// <summary>
        /// Uncategorized failure.
        /// </summary>
        public static readonly IAuthenticationResult Uncategorized = new AuthenticationResult(ResultCode.Uncategorized);

        /// <summary>
        /// Failure due to account lockout state.
        /// </summary>
        public static readonly IAuthenticationResult Lockout = new AuthenticationResult(ResultCode.Lockout);
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationResult"/> class.
        /// </summary>
        /// <param name="code">The result code</param>
        /// <param name="identity">The identity if authenticated, otherwise null</param>
        private AuthenticationResult(ResultCode code, Account identity = null)
        {
            this.Code = code;
            this.Identity = identity;
        }

        #endregion

        #region Internal Enums.

        /// <summary>
        /// The authorization result code.
        /// </summary>
        public enum ResultCode : int
        {
            /// <summary>
            /// General Failure.
            /// </summary>
            Failure = 0,

            /// <summary>
            /// Failure due to invalid credential being supplied.
            /// </summary>
            InvalidCredentials = 1,

            /// <summary>
            /// Failure due to identity being ambiguous.
            /// </summary>
            AmbigiousIdentity = 2,

            /// <summary>
            /// Failure due to identity not being found.
            /// </summary>
            NotFound = 3,

            /// <summary>
            /// Failure due to uncategorized reasons.
            /// </summary>
            Uncategorized = 4,

            /// <summary>
            /// Failure due to identity being locked out.
            /// </summary>
            Lockout = 5,

            /// <summary>
            /// Authentication success.
            /// </summary>
            Success = 6,
        }

        #endregion

        #region IAuthenticationResult Members.

        /// <inheritdoc />
        public bool IsAuthorized
        {
            get
            {
                return this.Code.Equals(ResultCode.Success);
            }
        }

        /// <inheritdoc />
        public bool IsGeneralFailure
        {
            get
            {
                return this.Code.Equals(ResultCode.Failure);
            }
        }

        /// <inheritdoc />
        public bool IsFailureDueToInvalidCredentials
        {
            get
            {
                return this.Code.Equals(ResultCode.InvalidCredentials);
            }
        }

        /// <inheritdoc />
        public bool IsFailureDueToAmbigiousIdentity
        {
            get
            {
                return this.Code.Equals(ResultCode.AmbigiousIdentity);
            }
        }

        /// <inheritdoc />
        public bool IsFailureDueToIdentityNotFound
        {
            get
            {
                return this.Code.Equals(ResultCode.NotFound);
            }
        }

        /// <inheritdoc />
        public bool IsFailureDueToUncategorizedReason
        {
            get
            {
                return this.Code.Equals(ResultCode.Uncategorized);
            }
        }

        /// <inheritdoc />
        public bool IsFailureDueToIdentityLockout
        {
            get
            {
                return this.Code.Equals(ResultCode.Lockout);
            }
        }

        /// <inheritdoc />
        public AuthenticationResult.ResultCode Code
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Account Identity
        {
            get;
            private set;
        }

        #endregion

        #region Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="AuthenticationResult"/> class.
        /// </summary>
        /// <param name="code">The result code</param>
        /// <param name="identity">The identity if authenticated, otherwise null</param>
        /// <returns>A new <see cref="IAuthenticationResult"/> instance</returns>
        public static IAuthenticationResult CreateNew(ResultCode code, Account identity = null)
        {
            return new AuthenticationResult(code, identity);
        }

        /// <summary>
        /// Creates a new authorization success result.
        /// </summary>
        /// <param name="account">The authenticated identity</param>
        /// <returns>A new successful <see cref="IAuthenticationResult"/> instance</returns>
        public static IAuthenticationResult CreateSuccessResult(Account account)
        {
            return new AuthenticationResult(ResultCode.Success, account);
        }

        #endregion
    }
}