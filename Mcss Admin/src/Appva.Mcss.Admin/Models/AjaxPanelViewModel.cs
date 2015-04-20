using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Web.ViewModels
{
    public class AjaxPanelViewModel
    {
        public string Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Header { get; set; }
    }
}