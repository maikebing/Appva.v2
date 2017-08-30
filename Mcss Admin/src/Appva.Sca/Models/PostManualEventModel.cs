using System;
using System.Collections.Generic;
using System.Text;

namespace Appva.Sca.Models
{
    public class PostManualEventModel
    {
        public string Id { get; set; }
        public string EventType { get; set; }
        public string ResidentId { get; set; }
        public string Timestamp { get; set; }
        public bool Active { get; set; }
    }
}
