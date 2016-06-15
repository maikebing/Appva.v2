using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Appva.Mvc;
using DataAnnotationsExtensions;
using Appva.Cqrs;
using Appva.Mcss.Admin.Models;

namespace Appva.Mcss.Web.ViewModels {

    public class EventViewModel : Identity<ListCalendar>
    {

        public EventViewModel() {
            AbsentSelectListItems = new List<SelectListItem>() {
                new SelectListItem() { Text = "Nej", Value = "0", Selected = true },
                new SelectListItem() { Text = "Ja", Value = "1" },
                new SelectListItem() { Text = "Ja, pausa alla stående ordinationer", Value = "2" }
            };
            Intervals = new List<SelectListItem>() {
                new SelectListItem() { Text = "Nej", Value = "0", Selected = true },
                new SelectListItem() { Text = "Veckovis", Value = "7" },
                new SelectListItem() { Text = "Månadsvis", Value = "31" },
                new SelectListItem() { Text = "Varje år", Value = "365" }
            };
            IntervalFactors = new List<SelectListItem>() {
                new SelectListItem() { Text = "1", Value = "1", Selected = true },
                new SelectListItem() { Text = "2", Value = "2" },
                new SelectListItem() { Text = "3", Value = "3" },
                new SelectListItem() { Text = "4", Value = "4" },
                new SelectListItem() { Text = "5", Value = "5" },
                new SelectListItem() { Text = "6", Value = "6" },
                new SelectListItem() { Text = "7", Value = "7" },
                new SelectListItem() { Text = "8", Value = "8" },
                new SelectListItem() { Text = "9", Value = "9" },
                new SelectListItem() { Text = "10", Value = "10" }
            };
        }

        public Guid PatientId { get; set; }
        public Guid SequenceId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime ChoosedDate { get; set; }
        public DateTime Date { get; set; }


        [DisplayName("Anteckning:")]
        public string Description { get; set; }

        [DisplayName("Kategori:")]
        [Required(ErrorMessage = "Kategori måste väljas")]
        public string Category { get; set; }

        [DisplayName("Ny kategori:")]
        [RequiredIf(Target = "Category", Value = "new", ErrorMessage="Namn på den nya kategorin måste fyllas i")]
        public string NewCategory { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThanOrEquals(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Startar:")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThanOrEquals(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DisplayName("Slutar:")]
        public DateTime EndDate { get; set; }

        [RequiredIf(Target = "AllDay", Value = false, ErrorMessage = "Tid måste fyllas i.")]
        [RegularExpression(@"^(\d{2}[:\.]\d{2})$", ErrorMessage = "Tid måste fyllas i med fyra siffror och kolon, t.ex. 14:30.")]
        [DisplayName("Starttid:")]
        [PlaceHolder("T.ex. 14:30")]
        public string StartTime { get; set; }

        [RequiredIf(Target = "AllDay", Value = false, ErrorMessage = "Tid måste fyllas i.")]
        [RegularExpression(@"^(\d{2}[:\.]\d{2})$", ErrorMessage = "Tid måste fyllas i med fyra siffror och kolon, t.ex. 14:30.")]
        [DisplayName("Sluttid:")]
        [PlaceHolder("T.ex. 14:30")]
        public string EndTime { get; set; }

        [DisplayName("Hela dagen?")]
        public bool AllDay { get; set; }

        [DisplayName("Upprepas?")]
        public int Interval { get; set; }

        [DisplayName("Upprepas var:")]
        public int IntervalFactor { get; set; }

        public bool SpecificDate { get; set; }

        [DisplayName("Frånvaro")]
        public bool Absent { get; set; }

        [DisplayName("Pausa alla stående ordinationer")]
        public bool PauseAnyAlerts { get; set; }

        [DisplayName("Visa på översikt")]
        public bool VisibleOnOverview { get; set; }

        [DisplayName("Kräver signering (larma vid försening)")]
        public bool Signable { get; set; }

        public IList<SelectListItem> AbsentSelectListItems { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Intervals { get; set; }
        public IList<SelectListItem> IntervalFactors { get; set; }

        public Dictionary<string, object> CalendarSettings { get; set; }

    }

}
