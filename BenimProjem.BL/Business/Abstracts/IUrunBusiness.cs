using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels.Urun;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IUrunBusiness
    {
        (bool, Urun) Ekle(UrunEkleDuzenleVM model, List<string> resimler);
        List<UrunListeVM> Listele();
        (bool, Urun) Sil(int Id,string path);
        (bool,Urun) Guncelle(UrunEkleDuzenleVM model, int urunId, List<string> resimler);
        Urun Urungetir(int Id);
        UrunEkleDuzenleVM UrunEkleDuzenleVMGetir(int Id);
        bool UrunResimleriSil(int urunId, string path);
        List<AnasayfaUrunleriListeleVM> AnasayfaUrunListele();
        List<Urun> UrunListesi();
        List<AnasayfaUrunleriListeleVM> AnasayfaKategoriyeGoreUrunListele(int? kategoriId); 
    }
}
