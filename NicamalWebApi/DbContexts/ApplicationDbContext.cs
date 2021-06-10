using Microsoft.EntityFrameworkCore;
using NicamalWebApi.Models;

namespace NicamalWebApi.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<User>().HasOne(a => a.Reported).WithOne(b => b.ReportedUser)
                .HasForeignKey<Report>(b => b.ReportedUserId);

            modelBuilder.Entity<Provinces>().HasData(
                new Provinces{ Id = 1, Name = "Álava"},
                new Provinces{ Id = 2, Name = "Albacete"},
                new Provinces{ Id = 3, Name = "Alicante"},
                new Provinces{ Id = 4, Name = "Almería"},
                new Provinces{ Id = 5, Name = "Asturias"},
                new Provinces{ Id = 6, Name = "Ávila"},
                new Provinces{ Id = 7, Name = "Badajoz"},
                new Provinces{ Id = 8, Name = "Barcelona"},
                new Provinces{ Id = 9, Name = "Burgos"},
                new Provinces{ Id = 10, Name = "Cáceres"},
                new Provinces{ Id = 11, Name = "Cádiz"},
                new Provinces{ Id = 12, Name = "Cantabria"},
                new Provinces{ Id = 13, Name = "Castellón"},
                new Provinces{ Id = 14, Name = "Ciudad Real"},
                new Provinces{ Id = 15, Name = "Córdoba"},
                new Provinces{ Id = 16, Name = "La Coruña"},
                new Provinces{ Id = 17, Name = "Cuenca"},
                new Provinces{ Id = 18, Name = "Girona"},
                new Provinces{ Id = 19, Name = "Granada"},
                new Provinces{ Id = 20, Name = "Guadalajara"},
                new Provinces{ Id = 21, Name = "Gipuzkoa"},
                new Provinces{ Id = 22, Name = "Huelva"},
                new Provinces{ Id = 23, Name = "Huesca"},
                new Provinces{ Id = 24, Name = "Baleares"},
                new Provinces{ Id = 25, Name = "Jaén"},
                new Provinces{ Id = 26, Name = "León"},
                new Provinces{ Id = 27, Name = "Lleida"},
                new Provinces{ Id = 28, Name = "Lugo"},
                new Provinces{ Id = 29, Name = "Madrid"},
                new Provinces{ Id = 30, Name = "Málaga"},
                new Provinces{ Id = 31, Name = "Murcia"},
                new Provinces{ Id = 32, Name = "Navarra"},
                new Provinces{ Id = 33, Name = "Ourense"},
                new Provinces{ Id = 34, Name = "Palencia"},
                new Provinces{ Id = 35, Name = "Las Palmas"},
                new Provinces{ Id = 36, Name = "Pontevedra"},
                new Provinces{ Id = 37, Name = "La Rioja"},
                new Provinces{ Id = 38, Name = "Salamanca"},
                new Provinces{ Id = 39, Name = "Segovia"},
                new Provinces{ Id = 40, Name = "Sevilla"},
                new Provinces{ Id = 41, Name = "Soria"},
                new Provinces{ Id = 42, Name = "Tarragona"},
                new Provinces{ Id = 43, Name = "Santa Crus de Tenerife"},
                new Provinces{ Id = 44, Name = "Teruel"},
                new Provinces{ Id = 45, Name = "Toledo"},
                new Provinces{ Id = 46, Name = "Valencia"},
                new Provinces{ Id = 47, Name = "Valladolid"},
                new Provinces{ Id = 48, Name = "Vizcaya"},
                new Provinces{ Id = 49, Name = "Zamora"},
                new Provinces{ Id = 50, Name = "Zaragoza"},
                new Provinces{ Id = 51, Name = "Ceuta"},
                new Provinces{ Id = 52, Name = "Melilla"}
                
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Disappearance> Disappearances { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Provinces> Provinces { get; set; }
        
    }
}