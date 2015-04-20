using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;

namespace Appva.Mcss.Web.ViewModels {

    public class ChangePasswordViewModel {

        [DisplayName("Nuvarande lösenord")]
        [Required(ErrorMessage = "Nuvarande lösenord måste fyllas i.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [DisplayName("Nytt lösenord")]
        [Required(ErrorMessage = "Nytt lösenord måste fyllas i.")]
        [MinLength(8, ErrorMessage = "Nytt lösenord måste vara minst 8 tecken långt.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Upprepa nytt lösenord")]
        [Required(ErrorMessage = "Upprepa lösenord måste fyllas i.")]
        [EqualTo("NewPassword", ErrorMessage = "Lösenord måste stämma överens.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }

}