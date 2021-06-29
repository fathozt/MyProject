using BenimProjem.Areas.yonetimPaneli.Controllers.Base;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenimProjem.PL.Areas.yonetimPaneli.Controllers
{
    public class UserController : BaseController
    {
        IUserManagerBusiness _userManagerBusiness;

        public UserController(IUserManagerBusiness userManagerBusiness)
        {
            _userManagerBusiness = userManagerBusiness;
        }

        public IActionResult Index()
        {
            return View(_userManagerBusiness.AllUsers());
        }
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var (result ,username) = await _userManagerBusiness.DeleteUser(Id);
            MesajYaz(result, username + " adlı kullanıcıı başarıyla silinmiştir", username + " adlı kullanıcı silinirken beklenmeyen bir hata oluştu ");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RolAta(int Id)
        {
            List<AppRoleAtaVM> roller = await _userManagerBusiness.TumRolleriGetir(Id);
            return View(roller);
        }
        [HttpPost]
        public async Task<IActionResult> RolAta(int Id, List<AppRoleAtaVM> model)
        {
            var (result, username) = await _userManagerBusiness.RolAtaveSil(Id, model);
            MesajYaz(result, username + " isimli kullanıcıya roller başarıyla atanmıştır.", username + " isimli kullanıcıya rol atanırken beklenmeyen bir hatayla karşılaşıldı.");
            return RedirectToAction("Index");
        }
    }
}
