
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
   public class Siparis
    {
        public int Id { get; set; }
        public int KullanıcıId { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public ICollection<SiparisDetay> Detaylar { get; set; }
    }
}
