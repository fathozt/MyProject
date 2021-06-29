using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.ViewModels.Urun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenimProjem.PL.Controllers
{
    public class UrunController : Controller
    {
        IUrunBusiness _urunBusiness;
        IKategoriBusiness _kategoriBusiness;

        public UrunController(IUrunBusiness urunBusiness, IKategoriBusiness kategoriBusiness)
        {
            _urunBusiness = urunBusiness;
            _kategoriBusiness = kategoriBusiness;
        }

        //if (kategoriId != null)
        //{
        //    urunler = _urunBusiness.AnasayfaKategoriyeGoreUrunListele(kategoriId.Value);
        //    ViewBag.KategoriAdi = _kategoriBusiness.KategoriGetir(kategoriId.Value) + "Ürünleri";
        //    return View(urunler);
        //}
        public IActionResult Index(int? kategoriId)
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            List<AnasayfaUrunleriListeleVM> urunler = null;
            if (kategoriId != null)
            {
                urunler = _urunBusiness.AnasayfaKategoriyeGoreUrunListele(kategoriId.Value);
                ViewBag.KategoriAdi = _kategoriBusiness.KategoriGetir(kategoriId.Value) + "Ürünleri";
                return View(urunler);
            }
            else
            {
                ViewBag.KategoriAdi = "Ürünler";
                return View(_urunBusiness.AnasayfaUrunListele());
            }
        }
        public IActionResult KategoriUrunleri()
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            return PartialView();
        }
    }
}
