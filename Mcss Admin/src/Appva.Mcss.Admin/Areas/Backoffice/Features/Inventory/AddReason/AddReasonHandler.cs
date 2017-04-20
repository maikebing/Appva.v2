// <copyright file="AddReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Fields
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    #endregion

    internal sealed class AddReasonHandler : RequestHandler<Parameterless<AddReasonModel>, AddReasonModel>
    {
        /// <summary>
        /// Handles the AddReasonModel
        /// </summary>
        /// <param name="message">The <see cref="AddReasonModel"/> message</param>
        /// <returns>A new <see cref="AddReasonModel"/></returns>
        public override AddReasonModel Handle(Parameterless<AddReasonModel> message)
        {
            return new AddReasonModel
            {

            };
        }
    }
}