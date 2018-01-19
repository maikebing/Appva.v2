namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Models;

    #endregion

    public class CreateAdministrationModel : IRequest<Parameterless<ListInventoriesModel>>
    {
        public CreateAdministrationModel()
        {
        }

        public string Name
        {
            get;
            set;
        }

        public List<SelectListItem> UnitSelectList
        {
            get;
            set;
        }

        public string SelectedUnit
        {
            get;
            set;
        }

        public string SpecificValues
        {
            get;
            set;
        }

        public double? Max
        {
            get;
            set;
        }

        public double? Min
        {
            get;
            set;
        }

        public double? Step
        {
            get;
            set;
        }

        public int? Fractions
        {
            get;
            set;
        }

        public bool IsCustomList
        {
            get;
            set;
        }
    }
}