using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Area51.Features.File.Details
{
    public class DetailsItemModel
    {        
        public PersonalIdentityNumber PersonalIdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IList<Role> MyProperty { get; set; }
        public Taxon Taxon { get; set; }
        public string HsaId { get; set; }
    }
}