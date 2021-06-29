using System;
using System.Collections;
using System.Collections.Generic;

namespace BenimProjem.Entities
{
    public class Kategori
    {
        public int Id { get; set; }
        public string KategoriAdi { get; set; }
        public ICollection<Urun> Urunler { get; set; }
    }
}
