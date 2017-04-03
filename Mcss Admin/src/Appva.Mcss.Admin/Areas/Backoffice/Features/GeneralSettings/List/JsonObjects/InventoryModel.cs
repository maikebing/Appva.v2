using System.Collections.Generic;
using System.ComponentModel;

namespace Appva.Mcss.Admin.Areas.Backoffice.JsonObjects
{
    public class InventoryObject
    {
        public string Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        public List<double> Amounts { get; set; }
        [DisplayName("Mängd")]
        public string Amount { get; set; }
    }
}