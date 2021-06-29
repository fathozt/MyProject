using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
   public class SiparisDetay
    {
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public int IndirimOrani { get; set; }
        public Urun Urun { get; set; }
        public Siparis Siparis { get; set; }
    }
}
