using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services;
using Appva.Mcss.Admin.Infrastructure;
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
            var patient = this.service.GetPatient(message.Id);
            var events = this.service.FindWithinMonth(patient, DateTime.Now);

            var model = new ViewTestCalendarModel();
            model.Patient = this.transformer.ToPatient(patient);


            return model;
        }
    }
}