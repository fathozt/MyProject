using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities.ViewModels.Urun
{
    public class UrunEkleDuzenleVM
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }
        public int MarkaId { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int StokAdeti { get; set; }
        public DateTime UretimTarihi { get; set; }
    }
}
