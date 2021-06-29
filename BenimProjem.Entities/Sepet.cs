using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
   public class Sepet
    {
        public int Id { get; set; }
        public int KullanıcıId { get; set; }
        public ICollection<SepetUrun> Urunler { get; set; }
    }
}
