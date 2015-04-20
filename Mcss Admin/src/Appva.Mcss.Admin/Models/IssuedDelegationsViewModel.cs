using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels
{
    public class IssuedDelegationsViewModel
    {
        public Guid AccountId { get; set; }
        public AccountViewModel Account { get; set; }
        public IList<Delegation> Delegations { get; set; }
        public bool History { get; set; }
    }
}