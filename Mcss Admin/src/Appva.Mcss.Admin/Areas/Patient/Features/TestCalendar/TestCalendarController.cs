// <copyright file="CalendarController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Patient.Features
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("patient"), RoutePrefix("{id:guid}/testcalendar")]
    public sealed class TestCalendarController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/>.</param>
        public TestCalendarController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Routes.

        [HttpGet]
        [Route("view/{scheduleid:guid}")]
        public ActionResult View(ViewTestCalendar request)
        {
            var view = this.mediator.Send(request);
            return View(view);
        }
        
        #endregion
    }
}