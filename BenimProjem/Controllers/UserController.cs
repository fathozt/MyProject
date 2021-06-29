using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.User;
using BenimProjem.PL.Controllers.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenimProjem.PL.Controllers
{
    public class UserController : BaseController
    {
        ISignInManagerBusiness _signInManagerBusiness;
        IUserManagerBusiness _userManagerBusiness;

        public UserController(IUserManagerBusiness userManagerBusiness, ISignInManagerBusiness signInManagerBusiness)
        {
            _userManagerBusiness = userManagerBusiness;
            _signInManagerBusiness = signInManagerBusiness;
        }
        public IActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(AppUserKayitOlVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool result = await _userManagerBusiness.CreateAsync(model);

            MesajYaz(result, "Kayıt işemi başarıyla gerçekleştirilmiştir", "Kayıt yapma işleminde beklenmeyen bir hata oluştu. Lütfen tekrar deneyiniz");

            if (!result)
                return View(model);
            else
                return RedirectToAction("GirisYap");
        }

        public IActionResult GirisYap(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(AppUserGirisYapVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            bool result = await _signInManagerBusiness.SignInAsync(model);

            if (result)
            {
                if (TempData["ReturnUrl"] != null)
                    return Redirect(TempData["ReturnUrl"].ToString());
                else
                    return RedirectToAction("Index", "Urun");
            }

            MesajYaz(result, "giriş başarılı", "kullanıcı adı veya email veya şifre yanlış");
            return View();
        }

        public async Task<IActionResult> CikisYap()
        {
            await _signInManagerBusiness.SignOutAsync();
            return RedirectToAction("Index", "Urun");
        }

        public IActionResult SifreTalep()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SifreTalep(AppUserSifreTalepVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (kullaniciResult, result) = await _userManagerBusiness.MailGonder(model.Email);
            MesajYaz(result, "Şifre yenilemek için kayıtlı mail adresinize bağlantı gönderilmiştir", !kullaniciResult ? "böyle bir mail adresi kayıtlı değil." : "mail gönderilirken beklenmeyen bir hata oluştu");
            return View();
        }

        public IActionResult SifreGuncelle(int userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SifreGuncelle(AppUserSifreGuncelleVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool result = await _userManagerBusiness.SifreGuncelle(int.Parse(TempData["userId"].ToString()), TempData["token"].ToString(), model);

            MesajYaz(result, "Şifreniz başarıyla değiştirildi", "Şifreniz değiştirilirken beklenmeyen bir hata oluştu");

            if (result)
                return RedirectToAction("GirisYap", "User");
            return View();
        }
    }
}
