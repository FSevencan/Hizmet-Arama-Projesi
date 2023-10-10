using Azure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proje.Models;

namespace Proje.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {

        public DbSet<UzmanProfil> UzmanProfilleri { get; set; }
        
        public DbSet<MusteriProfil> MusteriProfilleri { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<AltKategori> AltKategoriler { get; set; }
        public DbSet<Teklif> Teklifler { get; set; }
        public DbSet<TamamlanmisIs> TamamlanmisIsler { get; set; }
        public DbSet<UzmanYorum> UzmanYorumlari { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }
        public DbSet<Abone> Aboneler { get; set; }
        public DbSet<İletisim> İletisimMesajları { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Gonderen)
                .WithMany(u => u.GonderilenMesajlar)
                .HasForeignKey(m => m.GonderenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mesaj>()
                .HasOne(m => m.Alan)
                .WithMany(u => u.AlinanMesajlar)
                .HasForeignKey(m => m.AlanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teklif>()
                .HasOne(t => t.Uzman)
                .WithMany(u => u.Teklifler)
                .HasForeignKey(t => t.UzmanProfilId)
                .OnDelete(DeleteBehavior.Restrict); // UzmanProfil silindiğinde, ilişkilendirilmiş teklifler silinmez.

            modelBuilder.Entity<Teklif>()
                .HasOne(t => t.Musteri)
                .WithMany(m => m.Teklifler)
                .HasForeignKey(t => t.MusteriId)
                .OnDelete(DeleteBehavior.Restrict); // MusteriProfil silindiğinde, ilişkilendirilmiş teklifler silinmez.


            modelBuilder.Entity<UzmanYorum>()
                .HasOne(uy => uy.Musteri)
                .WithMany()
                .HasForeignKey(uy => uy.MusteriId)
                .OnDelete(DeleteBehavior.NoAction); // Musteri ile olan ilişkide cascade delete'i kapatıyoruz. Bir müşteri silindiginde yorumları silinmez.
        }




        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }





    }
}
