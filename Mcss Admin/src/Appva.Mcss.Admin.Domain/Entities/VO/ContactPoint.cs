// <copyright file="ContactPoint.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// Details for all kinds of technology-mediated contact points for a person or 
    /// organization, including telephone, email, etc.
    /// </summary>
    public abstract class ContactPoint : Primitive
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPoint"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        protected ContactPoint(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactPoint"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected ContactPoint()
        {
        }

        #endregion
    }
}