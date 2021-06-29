using BenimProjem.Entities;
using BenimProjem.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IKategoriBusiness
    {
        Kategori Ekle(KategoriEkleVeDuzenleVM model);
        List<KategoriListeVM> Listele();
        (bool, Kategori) Sil(int id);
        (bool, Kategori) Guncelle(KategoriEkleVeDuzenleVM model);
        Kategori KategoriGetir(int Id);
        KategoriEkleVeDuzenleVM KategoriEkleDuzenleVMGetir(int id);
        List<Urun> KategoriyeGoreUrunler(int Id);
    }
}
