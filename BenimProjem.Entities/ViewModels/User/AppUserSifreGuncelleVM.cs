using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenimProjem.Entities.ViewModels.User
{
    public class AppUserSifreGuncelleVM
    {
        [Required(ErrorMessage ="Lütfenboş geçmeyiniz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Lütfenboş geçmeyiniz")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor")]
        public string PasswordConfirm { get; set; }
    }
}
