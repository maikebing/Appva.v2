using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Appva.Mvc.Html.DataAnnotations;

namespace Appva.Mcss.Web.ViewModels {

    public class DateSpanViewModel {

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateLessThan(Target = "EndDate", ErrorMessage = "Startdatum måste vara ett tidigare datum är slutdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DataType(DataType.Date)]
        [DisplayName("Från")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Datum måste fyllas i.")]
        [Date(ErrorMessage = "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.")]
        [DateGreaterThan(Target = "StartDate", ErrorMessage = "Slutdatum måste vara ett senare datum är startdatum.")]
        [PlaceHolder("T.ex. 2012-12-21")]
        [DataType(DataType.Date)]
        [DisplayName("Till")]
        public DateTime EndDate { get; set; }

    }

}