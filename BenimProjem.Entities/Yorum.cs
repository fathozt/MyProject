using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities
{
    public class Yorum
    {
        public int Id { get; set; }
        public int UrunId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Mail { get; set; }
        public string Mesaj { get; set; }
        public Urun Urun { get; set; }
    }
}
