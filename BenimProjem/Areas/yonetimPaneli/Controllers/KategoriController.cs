using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.Areas.yonetimPaneli.Controllers.Base;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenimProjem.Areas.yonetimPaneli.Controllers
{
    public class KategoriController : BaseController
    {
        IKategoriBusiness _kategoriBusiness;
        public KategoriController(IKategoriBusiness kategoriBusiness)
        {
            _kategoriBusiness = kategoriBusiness;
        }

        [Area("yonetimPaneli")]
        [Authorize]
        public IActionResult Index()
        {
            var kategoriler = _kategoriBusiness.Listele();
            return View(kategoriler);
        }
        public IActionResult Ekle(int? Id)
        {
            if (Id != null)
            {
                KategoriEkleVeDuzenleVM guncellenecekKategori = _kategoriBusiness.KategoriEkleDuzenleVMGetir(Id.Value);

                return View(guncellenecekKategori);
            }
            return View();

        }

        [HttpPost]
        public IActionResult Ekle(int? Id, KategoriEkleVeDuzenleVM model)
        {
            if (Id != null)
            {
                //Duzenleme
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var (sonuc, kategori) = _kategoriBusiness.Guncelle(model);
                MesajYaz(sonuc, kategori.KategoriAdi + " isimli kategori " + model.KategoriAdi + " isimiyle başarıyla değiştirilmiştir", kategori.KategoriAdi + " isimli kategori " + model.KategoriAdi + " isimli kategoriyle değiştirilirken beklenmeyen bir hata oluştu");
                return RedirectToAction("Index");
            }
            else
            {
                //Ekleme
                if (ModelState.IsValid)
                {
                    _kategoriBusiness.Ekle(model);
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }
        public JsonResult Sil(int id)
        {
            var (sonuc, kategori) = _kategoriBusiness.Sil(id);
            
                return Json(new {
                    result = sonuc, 
                    kategoriAdi = kategori.KategoriAdi
                });
        }

        public JsonResult SilKontrol(int Id)
        {
            List<Urun> urunListesi = _kategoriBusiness.KategoriyeGoreUrunler(Id);

            Kategori kategori = _kategoriBusiness.KategoriGetir(Id);
            
            if (urunListesi.Count > 0)
                return Json(new
                {
                    any = true,
                    urunler = urunListesi.Select(u => new
                    {
                        id = u.Id,
                        kategoriAdi = u.Kategori.KategoriAdi,
                        urunAdi = u.UrunAdi,
                        kategoriId = u.KategoriId,
                    })
                });
            else
                return Json(new {
                    kategoriAdi = kategori.KategoriAdi,
                    any = false
                });
        }
    }
}
