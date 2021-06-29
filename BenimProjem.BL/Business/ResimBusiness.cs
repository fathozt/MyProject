using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Repositories.Abstracts;
using BenimProjem.BL.Repositories.Concretes;
using BenimProjem.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business
{
    public class ResimBusiness : IResimBusiness
    {
        IResimRepository _resimRepository;
        public ResimBusiness()
        {
            _resimRepository = new ResimRepository();
        }
        public bool Ekle(List<string> resimler, int urunId)
        {
            try
            {
                foreach (var resim in resimler)
                {
                    Resim resimObj = new Resim()
                    {
                        ResimAdi = resim,
                        UrunId = urunId
                    };
                    _resimRepository.Add(resimObj);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ResimleriFizikselSil(List<string> resimler, string path)
        {
            try
            {
                foreach (var resim in resimler)
                {
                    File.Delete(path + "\\Images\\" + resim);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ResimSil(int resimId)
        {
            Resim resim = _resimRepository.GetSingle(r => r.Id == resimId);
            return _resimRepository.Remove(resim);
        }

        public async Task<(bool, List<string>)> Upload(List<IFormFile> files, string path, string dosyaAdi)
        {
            string uploadPath = path +"\\Images\\";
            List<string> resimler = new List<string>();
            try
            {
                int i = 0;
                foreach (IFormFile file in files)
                {
                    resimler.Add(dosyaAdi +"-"+ i + Path.GetExtension(file.FileName));
                    FileStream fileStream = new FileStream(uploadPath + "\\" + dosyaAdi +"-"+ i + Path.GetExtension(file.FileName), FileMode.Create);
                    await file.CopyToAsync(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();
                    i++;
                }
                return (true, resimler);
            }
            catch (Exception)
            {
                return (false, resimler);
            }
        }

        public List<Resim> UruneGoreResimListesiGetir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
