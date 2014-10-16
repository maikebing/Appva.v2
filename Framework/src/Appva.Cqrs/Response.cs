// <copyright file="Response.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cqrs
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Response : Response<Unit>
    {
        #region Response<Unit> Overrides.

        /// <summary>
        /// Returns default unit.
        /// </summary>
        public override Unit Data
        {
            get
            {
                return Unit.Value;
            }
            set
            {
            }
        }

        #endregion
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Response<TResponseData>
    {

        /// <summary>
        /// The message response data.
        /// </summary>
        public virtual TResponseData Data
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Exception"/>.
        /// </summary>
        public virtual Exception Exception
        {
            get;
            set;
        }

        /// <summary>
        /// Whether the response has an exception.
        /// </summary>
        /// <returns>True if an <see cref="Exception"/> was raised</returns>
        public virtual bool HasException()
        {
            return Exception != null;
        }
    }
}