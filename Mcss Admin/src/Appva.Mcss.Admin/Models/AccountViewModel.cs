using System;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels {

    public class AccountViewModel {

        public virtual Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string UniqueIdentifier { get; set; }

        public string Title { get; set; }

        public string Superior { get; set; }

        public bool ShowAlertOnDaysLeft { get; set; }

        public int? DaysLeft { get; set; }

        public bool Active { get; set; }

        public bool IsPaused { get; set; }

        public Account Account { get; set; }

    }

}