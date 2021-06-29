using BenimProjem.Areas.yonetimPaneli.Controllers.Base;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenimProjem.PL.Areas.yonetimPaneli.Controllers
{
    [Area("yonetimPaneli")]
    public class RoleController : BaseController
    {
        IRoleManagerBusiness _roleManagerBusiness;

        public RoleController(IRoleManagerBusiness roleManagerBusiness)
        {
            _roleManagerBusiness = roleManagerBusiness;
        }

        public IActionResult Index()
        {
            return View(_roleManagerBusiness.RolleriGetir());
        }

        public async Task<IActionResult> Sil(int Id)
        {
            var (result, role) = await _roleManagerBusiness.RoleSilAsync(Id);
            MesajYaz(result, role.Name + " isimli rol başarıyla silinmiştir.", role.Name + " isimli role silinirken beklenmeyen bir hata oluştu.");
            return RedirectToAction("Index");
        }

        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(AppRoleEkleVM model)
        {
            bool result = await _roleManagerBusiness.RoleEkleAsync(model);
            MesajYaz(result, model.Name + " isimli rol başarıyla eklenmiştir", model.Name + " isimli role eklenirken beklenmeyen bir hata oluştu");
            return RedirectToAction("Index");
        }
    }
}
