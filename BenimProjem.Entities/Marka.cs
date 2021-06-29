using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
    public class Marka
    {
        public int Id { get; set; }
        public string MarkaAdi { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }
}
