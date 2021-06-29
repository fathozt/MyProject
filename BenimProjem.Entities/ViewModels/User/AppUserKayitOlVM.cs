using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenimProjem.Entities.ViewModels.User
{
    public class AppUserKayitOlVM
    {
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        [EmailAddress(ErrorMessage ="Lütfen email adresinizi doğru formatta giriniz")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Lütfen boş geçmeyiniz")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor!")]
        [Required(ErrorMessage = "Lütfen boş geçmeyiniz")]
        public string PasswordConfirm { get; set; }

    }
}
