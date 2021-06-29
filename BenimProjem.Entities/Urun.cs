using System;
using System.Collections;
using System.Collections.Generic;

namespace BenimProjem.Entities
{
    public class Urun
    {
        public int Id { get; set; }
        public int KategoriId { get; set; }
        public int MarkaId { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunFiyati { get; set; }
        public int StokAdeti { get; set; }
        public DateTime UretimTarihi { get; set; }

        public Kategori Kategori { get; set; }
        public Marka Marka { get; set; }
        public ICollection<Resim> Resimler { get; set; }
        public ICollection<Yorum> Yorumlar { get; set; }
        public ICollection<SepetUrun> Sepetler { get; set; }
        public ICollection<SiparisDetay> Siparisler { get; set; }

    }
}
