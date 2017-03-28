using System;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class AccountViewModel {

        public virtual Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public PersonalIdentityNumber UniqueIdentifier { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public bool IsPaused { get; set; }

        public bool IsEditableForCurrentPrincipal
        {
            get;
            set;
        }
    }

}