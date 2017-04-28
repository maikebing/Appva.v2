// <copyright file="DeviceDetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    public class DeviceDetailsHandler : RequestHandler<Identity<DeviceDetails>, DeviceDetails>
    {
        public override DeviceDetails Handle(Identity<DeviceDetails> message)
        {
            throw new NotImplementedException();
        }
    }
}