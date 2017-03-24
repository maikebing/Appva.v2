using Appva.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class EditGeneralSettingsModel : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string MachineName { get; set; }

        public int? intValue { get; set; }

        public string stringValue { get; set; }

        public bool? boolValue { get; set; }

        public IList<int> listValue { get; set; }

        public string Name { get; set; }


    }
}