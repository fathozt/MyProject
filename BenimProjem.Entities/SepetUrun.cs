using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
   public class SepetUrun
    {
        public int UrunId { get; set; }
        public int SepetId { get; set; }

        public Sepet Sepet { get; set; }
        public Urun Urun { get; set; }
    }
}
