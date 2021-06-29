using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities.ViewModels.Urun
{
    public class AnasayfaUrunleriListeleVM
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public string KategoriAdi { get; set; }
        public string Adi { get; set; }
        public decimal Fiyat { get; set; }
        public List<string> Resimler { get; set; }
    }
}
