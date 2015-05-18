// <copyright file="AlertDangerAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    /// <summary>
    /// Adds a danger alert message to the temp data.
    /// </summary>
    public sealed class AlertDangerAttribute : AbstractAlertAttribute
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertDangerAttribute"/> class.
        /// </summary>
        /// <param name="message">The message to convey</param>
        public AlertDangerAttribute(string message)
            : base(message)
        {
        }

        #endregion

        #region AbstractAlertAttribute Overrides.

        /// <inheritdoc />
        protected override string GetContext()
        {
            return "alert alert-danger";
        }

        #endregion
    }
}