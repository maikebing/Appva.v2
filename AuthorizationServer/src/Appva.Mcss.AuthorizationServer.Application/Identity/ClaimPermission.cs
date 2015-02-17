// <copyright file="ClaimPermission.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application.Identity
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    public sealed class ClaimPermission : IPermission
    {
        #region Variables.

        private string context;
        private string resource;
        private string action;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimPermission"/> class.
        /// </summary>
        public ClaimPermission(string context, string resource, string action)
        {
            this.context = context;
            this.resource = resource;
            this.action = action;
        }

        #endregion

        #region IPermission Members

        public IPermission Copy()
        {
            throw new NotImplementedException();
        }

        public void Demand()
        {
            throw new NotImplementedException();
        }

        public IPermission Intersect(IPermission target)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IPermission target)
        {
            throw new NotImplementedException();
        }

        public IPermission Union(IPermission target)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISecurityEncodable Members

        public void FromXml(SecurityElement e)
        {
            throw new NotImplementedException();
        }

        public SecurityElement ToXml()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}