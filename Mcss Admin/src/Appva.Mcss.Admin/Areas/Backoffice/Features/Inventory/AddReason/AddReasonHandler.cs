// <copyright file="AddReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
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