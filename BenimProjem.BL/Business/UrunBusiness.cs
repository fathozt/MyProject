using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Repositories.Abstracts;
using BenimProjem.BL.Repositories.Concretes;
using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels.Urun;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenimProjem.BL.Business
{
    public class UrunBusiness : IUrunBusiness
    {
        IUrunRepository _urunRepository;
        IResimRepository _resimRepository;
        IResimBusiness _resimBusiness;
        public UrunBusiness()
        {
            _urunRepository = new UrunRepository();
            _resimRepository = new ResimRepository();
            _resimBusiness = new ResimBusiness();
        }

        public List<AnasayfaUrunleriListeleVM> AnasayfaUrunListele()
        {
            return _urunRepository.GetTable().Include(u => u.Marka).Include(r => r.Resimler).Include(u => u.Kategori).ToList().Select(u =>
              {
                  List<string> resimAdiListesi = UruneGoreResimListesiGetir(u.Id);

                  return new AnasayfaUrunleriListeleVM
                  {
                      Adi = u.UrunAdi,
                      Fiyat = u.UrunFiyati,
                      KategoriAdi = u.Kategori.KategoriAdi,
                      MarkaAdi = u.Marka.MarkaAdi,
                      Resimler = resimAdiListesi,
                  };
              }).ToList();
        }

        List<string> UruneGoreResimListesiGetir(int id)
        {
            List<string> urunResimleriListesi = null;
            Urun urun = Urungetir(id);
            urunResimleriListesi = urun.Resimler.Select(r => r.ResimAdi).ToList();

            return urunResimleriListesi;
        }
        public (bool, Urun) Ekle(UrunEkleDuzenleVM model, List<string> resimler)
        {
            List<Resim> eklenecekResimler = new List<Resim>();
            foreach (var resim in resimler)
            {
                eklenecekResimler.Add(new Resim
                {
                    ResimAdi = resim
                });
            }

            Urun urun = _urunRepository.Add(new Urun
            {
                KategoriId = model.KategoriId,
                MarkaId = model.MarkaId,
                UrunAdi = model.UrunAdi,
                UrunFiyati = model.UrunFiyati,
                StokAdeti = model.StokAdeti,
                UretimTarihi = model.UretimTarihi,
                Resimler = eklenecekResimler
            });
            _urunRepository.Update(urun);
            return (true, urun);
        }

        public (bool, Urun) Guncelle(UrunEkleDuzenleVM model, int urunId, List<string> resimler)
        {
            List<Resim> eklenecekResimler = new List<Resim>();
            foreach (var resim in resimler)
            {
                eklenecekResimler.Add(new Resim
                {
                    ResimAdi = resim
                });
            }

            Urun guncellenecekUrun = _urunRepository.GetSingle(u => u.Id == model.Id);
            string guncellenmemisAdi = model.UrunAdi;
            guncellenecekUrun.UrunAdi = model.UrunAdi;
            guncellenecekUrun.UrunFiyati = model.UrunFiyati;
            guncellenecekUrun.UretimTarihi = model.UretimTarihi;
            guncellenecekUrun.StokAdeti = model.StokAdeti;
            guncellenecekUrun.MarkaId = model.MarkaId;
            guncellenecekUrun.KategoriId = model.KategoriId;
            guncellenecekUrun.Resimler = eklenecekResimler;
            var sonuc = _urunRepository.Update(guncellenecekUrun);
            guncellenecekUrun.UrunAdi = guncellenmemisAdi;

            return (sonuc, guncellenecekUrun);
        }

        public List<UrunListeVM> Listele()
        {
            return _urunRepository.GetTable().Include(m => m.Marka).Include(k => k.Kategori).Select(u => new UrunListeVM
            {
                Id = u.Id,
                MarkaAdi = u.Marka.MarkaAdi,
                KategoriAdi = u.Kategori.KategoriAdi,
                StokAdet = u.StokAdeti,
                UrunAdi = u.UrunAdi,
                UrunFiyati = u.UrunFiyati
            }).ToList();
        }

        public List<Urun> UrunListesi()
        {
            return _urunRepository.GetTable().Include(m => m.Marka).Include(k => k.Kategori).Include(r => r.Resimler).ToList();
        }

        public (bool, Urun) Sil(int Id, string path)
        {
            Urun silinecekUrun = _urunRepository.GetTable().Include(r => r.Resimler).FirstOrDefault(u => u.Id == Id);
            _resimBusiness.ResimleriFizikselSil(silinecekUrun.Resimler.Select(r => r.ResimAdi).ToList(), path);
            var sonuc = _urunRepository.Remove(silinecekUrun);
            return (sonuc, silinecekUrun);
        }

        public UrunEkleDuzenleVM UrunEkleDuzenleVMGetir(int Id)
        {
            Urun urun = Urungetir(Id);
            return new UrunEkleDuzenleVM
            {
                UrunAdi = urun.UrunAdi,
                KategoriId = urun.KategoriId,
                MarkaId = urun.MarkaId,
                StokAdeti = urun.StokAdeti,
                UretimTarihi = urun.UretimTarihi,
                UrunFiyati = urun.UrunFiyati
            };
        }

        public Urun Urungetir(int Id)
        {
            return _urunRepository.GetTable().Include(m => m.Marka).Include(k => k.Kategori).Include(r => r.Resimler).FirstOrDefault(u => u.Id == Id);
        }

        public bool UrunResimleriSil(int urunId, string path)
        {
            try
            {
                List<Resim> urunResimler = _resimRepository.GetWhere(r => r.UrunId == urunId);
                _resimBusiness.ResimleriFizikselSil(urunResimler.Select(r => r.ResimAdi).ToList(), path);
                foreach (var resim in urunResimler)
                {
                    _resimBusiness.ResimSil(resim.Id);
                }
                return true;

            }
            catch
            {
                return false;
            }
        }

        public List<AnasayfaUrunleriListeleVM> AnasayfaKategoriyeGoreUrunListele(int? kategoriId)
        {
          return  _urunRepository.GetTable().Include(m => m.Marka).Include(k => k.Kategori).Include(r => r.Resimler).Where(k=>k.KategoriId == kategoriId.Value).ToList().Select(u => {

                List<string> resimAdiListesi = UruneGoreResimListesiGetir(u.Id);

                return new AnasayfaUrunleriListeleVM
                {
                    Adi = u.UrunAdi,
                    Fiyat = u.UrunFiyati,
                    KategoriAdi = u.Kategori.KategoriAdi,
                    MarkaAdi = u.Marka.MarkaAdi,
                    Resimler = resimAdiListesi,
                };
            }).ToList();
        } 
    }
}
