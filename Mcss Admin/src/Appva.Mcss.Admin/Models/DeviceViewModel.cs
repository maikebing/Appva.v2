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

        public int Version
        {
            get; set;
        }

        public DateTime CreatedAt
        {
            get;
            set;
        }
        public DateTime Modified
        {
            get; set;
        }

        public DateTime? LastPingedDate
        {
            get; set;
        }

        public DateTime LastUsedDate
        {
            get; set;
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

        public string AzurePushId
        {
            get; set;
        }

        public string UDID
        {
            get; set;
        }


        public string Uuid
        {
            get; set;
        }

        

        public string PushUuid
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }



    }
}