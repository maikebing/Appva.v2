// <copyright file="DelegationTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.ResourceServer.Transformers
{
    #region Imports

    using System.Collections.Generic;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Models;

    #endregion

    /// <summary>
    /// Delegation transforming.
    /// </summary>
    public class DelegationTransformer
    {
        /// <summary>
        /// Transforms a <see cref="Delegation"/> to a <see cref="DelegationModel"/>
        /// </summary>
        /// <param name="delegation">The <see cref="Delegation"/> to be transformed</param>
        /// <returns>A <see cref="DelegationModel"/></returns>
        public static DelegationModel ToDelegation(Delegation delegation)
        {
            return new DelegationModel
            {
                Id = delegation.Id,
                Name = delegation.Name
            };
        }

        /// <summary>
        /// Transforms a collection of <see cref="Delegation"/> to a
        /// colleaction of <see cref="DelegationModel"/>
        /// </summary>
        /// <param name="delegations">The <see cref="IList{Delegation}"/> to be transformed</param>
        /// <returns>An <see cref="IList{DelegationModel}"/></returns>
        public static IList<DelegationModel> ToDelegation(IList<Delegation> delegations)
        {
            var retval = new List<DelegationModel>();
            foreach (var delegation in delegations)
            {
                retval.Add(ToDelegation(delegation));
            }
            return retval;
        }
    }
}