using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities.ViewModels.Urun
{
    public class UrunListeVM
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public string KategoriAdi { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int StokAdet { get; set; }
    }
}
