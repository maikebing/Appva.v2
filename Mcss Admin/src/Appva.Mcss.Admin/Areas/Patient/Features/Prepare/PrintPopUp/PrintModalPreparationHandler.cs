// <copyright file="PrintModalPreparationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PrintModalPreparationHandler : RequestHandler<PrintModalPreparation, PreparePrintPopUpViewModel>
    {
        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override PreparePrintPopUpViewModel Handle(PrintModalPreparation message)
        {
            return new PreparePrintPopUpViewModel
            {
                Id = message.Id,
                ScheduleId = message.ScheduleId,
                PrintStartDate = message.Date.FirstOfMonth(),
                PrintEndDate = message.Date.LastOfMonth()
            };
        }

        #endregion
    }
}