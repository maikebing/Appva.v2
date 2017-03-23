using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class ListGeneralSettingsModel
    {
        public IEnumerable<Setting> List { get; set; }
    }
}