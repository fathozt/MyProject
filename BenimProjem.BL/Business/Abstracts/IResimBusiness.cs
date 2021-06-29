using BenimProjem.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IResimBusiness
    {
        Task<(bool, List<string>)> Upload(List<IFormFile> files, string path, string dosyaAdi);
        bool Ekle(List<string> resimler, int urunId);
        bool ResimleriFizikselSil(List<string> resimler, string path);//Fiziksel sil
        bool ResimSil(int resimId);//Veritabanından sil
        List<Resim> UruneGoreResimListesiGetir(int id);

    }
}
