using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Repositories.Abstracts;
using BenimProjem.BL.Repositories.Concretes;
using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenimProjem.BL.Business
{
    public class KategoriBusiness : IKategoriBusiness
    {
        IKategoriRepository _kategoriRepository;
        IUrunRepository _urunRepository;
        IUrunBusiness _urunBusiness;
        public KategoriBusiness()
        {
            _kategoriRepository = new KategoriRepository();
            _urunRepository = new UrunRepository();
            _urunBusiness = new UrunBusiness();
        }

        public KategoriEkleVeDuzenleVM KategoriEkleDuzenleVMGetir(int id)
        {
            Kategori kategori = KategoriGetir(id);
            KategoriEkleVeDuzenleVM kategoriDuzenleVM = new KategoriEkleVeDuzenleVM
            {
                Id = kategori.Id,
                KategoriAdi = kategori.KategoriAdi
            };

            return kategoriDuzenleVM;
        }

        public Kategori Ekle(KategoriEkleVeDuzenleVM model)
        {
            return _kategoriRepository.Add(new Kategori
            {
                Id = model.Id,
                KategoriAdi = model.KategoriAdi
            });
        }

        public (bool, Kategori) Guncelle(KategoriEkleVeDuzenleVM model)
        {
            Kategori guncellenecekKategori = _kategoriRepository.GetSingle(k => k.Id == model.Id);
            string guncellenmemisAdi = guncellenecekKategori.KategoriAdi;
            guncellenecekKategori.KategoriAdi = model.KategoriAdi;
            var sonuc = _kategoriRepository.Update(guncellenecekKategori);
            guncellenecekKategori.KategoriAdi = guncellenmemisAdi;
            return (sonuc, guncellenecekKategori);
        }

        public Kategori KategoriGetir(int Id)
        {

            return _kategoriRepository.GetSingle(k => k.Id == Id);
        }

        public List<KategoriListeVM> Listele()
        {
            return _kategoriRepository.Get().Select(k => new KategoriListeVM
            {
                Id = k.Id,
                KategoriAdi = k.KategoriAdi,
                UrunAdet = _urunBusiness.AnasayfaKategoriyeGoreUrunListele(k.Id).Count
            }).ToList();
        }
         
        public (bool, Kategori) Sil(int id)
        {
            Kategori silinecekKategori = _kategoriRepository.GetSingle(k => k.Id == id);
            return ((_kategoriRepository.Remove(silinecekKategori), silinecekKategori));
        }

        /*GetWhere(x => x.KategoriId == Id);*/
        //.ToList().Select(k => k.KategoriId == Id).ToList()
        public List<Urun> KategoriyeGoreUrunler(int Id)
        {
        List<Urun> urunListesi = _urunRepository.GetTable().Include(k => k.Kategori).Where(k => k.KategoriId == Id).ToList();
            return urunListesi;
        }
    }
}
