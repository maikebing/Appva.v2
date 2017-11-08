using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure;
using Appva.Mcss.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models.Handlers
{
    public class ViewTestCalendarHandler : RequestHandler<ViewTestCalendar, ViewTestCalendarModel>
    {
        private readonly ICalendarService service;
        private readonly IPatientTransformer transformer;

        public ViewTestCalendarHandler(ICalendarService service, IPatientTransformer transformer)
        {
            this.service = service;
            this.transformer = transformer;
        }

        public override ViewTestCalendarModel Handle(ViewTestCalendar message)
        {
            var date = message.Date.HasValue ? message.Date.Value.FirstOfMonth() : DateTime.Today.FirstOfMonth();
            if (message.Prev.IsNotNull())
            {
                date = message.Date.GetValueOrDefault().PreviousMonth();
            }
            if (message.Next.IsNotNull())
            {
                date = message.Date.GetValueOrDefault().NextMonth();
            }

            var schedule = this.service.GetSchedule(message.ScheduleId); // done
            var events = this.service.FindSequencesWithinMonth(schedule, date); // skall skicka med schedule..
            var categories = this.service.GetCategories(); // done
            var calendar = this.service.Calendar(date, events); // done

            var model = new ViewTestCalendarModel();
            model.Patient = this.transformer.ToPatient(schedule.Patient);
            model.Current = date;
            model.Previous = date.PreviousMonth();
            model.Next = date.NextMonth();
            model.Calendar = calendar;
            // model.Categories = categories.ToDictionary(x => categories.IndexOf(x));
            model.CategorySettings = categories;

            return model;
        }
    }
}