using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
    public class Resim
    {
        public int Id { get; set; }
        public int UrunId { get; set; }
        public string ResimAdi { get; set; }
        public Urun Urun { get; set; }
    }
}
