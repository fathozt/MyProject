using BenimProjem.Entities;
using BenimProjem.Entities.Authentications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.DAL
{
    public class BenimProjemDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public BenimProjemDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }   
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetay> SiparisDetaylar { get; set; }
        public DbSet<SepetUrun> SpetUrunler { get; set; }
        public DbSet<Resim> Resimler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>()
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Urunler)
                .HasForeignKey(k => k.KategoriId);

            modelBuilder.Entity<Yorum>()
                .HasOne(u => u.Urun)
                .WithMany(y => y.Yorumlar)
                .HasForeignKey(u => u.UrunId);

            modelBuilder.Entity<Urun>()
                .HasOne(m => m.Marka)
                .WithMany(u => u.Urunler)
                .HasForeignKey(m => m.MarkaId);

            modelBuilder.Entity<Resim>()
                .HasOne(u => u.Urun)
                .WithMany(m => m.Resimler)
                .HasForeignKey(u => u.UrunId);

            modelBuilder.Entity<SiparisDetay>().HasKey(k => new { k.SiparisId, k.UrunId });

            modelBuilder.Entity<SiparisDetay>()
                .HasOne(u => u.Urun)
                .WithMany(s => s.Siparisler)
                .HasForeignKey(u => u.UrunId);

            modelBuilder.Entity<SiparisDetay>()
                .HasOne(s => s.Siparis)
                .WithMany(d => d.Detaylar)
                .HasForeignKey(u => u.SiparisId);

            modelBuilder.Entity<SepetUrun>().HasKey(k => new { k.SepetId, k.UrunId });

            modelBuilder.Entity<SepetUrun>()
                .HasOne(u => u.Urun)
                .WithMany(s => s.Sepetler)
                .HasForeignKey(u => u.UrunId);

            modelBuilder.Entity<SepetUrun>()
                .HasOne(s => s.Sepet)
                .WithMany(u => u.Urunler)
                .HasForeignKey(u => u.SepetId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
