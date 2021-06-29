using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.Areas.yonetimPaneli.Controllers.Base;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Repositories.Abstracts;
using BenimProjem.BL.Repositories.Concretes;
using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels.Urun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BenimProjem.PL.Areas.yonetimPaneli.Controllers
{
    public class UrunController : BaseController
    {

        IUrunBusiness _urunBusiness;
        IKategoriBusiness _kategoriBusiness;
        IMarkaBusiness _markaBusiness;
        IWebHostEnvironment _webHostEnvironment;
        IResimBusiness _resimBusiness;
        public UrunController(IUrunBusiness urunBusiness,
            IKategoriBusiness kategoriBusiness,
            IMarkaBusiness markaBusiness,
            IWebHostEnvironment webHostEnvironment,
            IResimBusiness resimBusiness)
        {
            _urunBusiness = urunBusiness;
            _kategoriBusiness = kategoriBusiness;
            _markaBusiness = markaBusiness;
            _webHostEnvironment = webHostEnvironment;
            _resimBusiness = resimBusiness;
        }
        [Area("yonetimPaneli")]
        [Authorize]

        public IActionResult Index()
        {
            return View(_urunBusiness.Listele());
        }
        public IActionResult Ekle(int? Id)
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            ViewBag.Markalar = _markaBusiness.Listele();
            if (Id != null)
            {
                UrunEkleDuzenleVM guncellenecekUrun = _urunBusiness.UrunEkleDuzenleVMGetir(Id.Value);
                return View(guncellenecekUrun);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Ekle(int? Id, UrunEkleDuzenleVM model, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Kategoriler = _kategoriBusiness.Listele();
                ViewBag.Markalar = _markaBusiness.Listele();
                return View(model);
            }
            if (Id != null)
            {
                //Duzenleme
                _urunBusiness.UrunResimleriSil(Id.Value, _webHostEnvironment.WebRootPath);
            }
            var (sonuc1, resimler) = await _resimBusiness.Upload(files, _webHostEnvironment.WebRootPath, model.UrunAdi);
            if (Id != null)
            {
                var (sonuc, urun) = _urunBusiness.Guncelle(model, Id.Value, resimler);
                MesajYaz(sonuc, urun.UrunAdi + " isimli urun " + model.UrunAdi + " isimli urunle başarıyla değiştirilmiştir", urun.UrunAdi + " isimli urun " + model.UrunAdi + " isimli urunle değiştirilirken beklenmeyen bir hata oluştu");
            }
            else
            {
                _urunBusiness.Ekle(model, resimler);
                MesajYaz(true, model.UrunAdi + " isimli ürün başarıyla güncellenmiştir.", model.UrunAdi + "isimli ürün güncellenirken beklenmeyen bir hatayla karşılaşılmıştır.");
            }
            return RedirectToAction("Index");
        }

        //public IActionResult Sil(int Id)
        //{
        //    var (sonuc, Urun) = _urunBusiness.Sil(Id, _webHostEnvironment.WebRootPath);
        //    MesajYaz(sonuc, Urun.UrunAdi + " isimli urun basarıla silinmiştir", Urun.UrunAdi + " isimli urun silinirken beklenmeyen bir haya oluştu");
        //    return RedirectToAction("Index");
        //}

        public JsonResult Sil(int? Id)
        {
            var (sonuc, Urun) = _urunBusiness.Sil(Id.Value, _webHostEnvironment.WebRootPath);
            return Json(sonuc);
        }
    }
}
