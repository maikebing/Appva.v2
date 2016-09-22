using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class DeviceViewModel
    {
        public bool IsActive
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string OS
        {
            get;
            set;
        }

        public string OSVersion
        {
            get;
            set;
        }

        public string AppBundle
        {
            get;
            set;
        }

        public string AppVersion
        {
            get;
            set;
        }

        public string Hardware
        {
            get;
            set;
        }
    }
}