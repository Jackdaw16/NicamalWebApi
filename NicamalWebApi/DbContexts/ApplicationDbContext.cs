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

            modelBuilder.Entity<User>().HasOne(a => a.Reported).WithOne(b => b.ReportedUser)
                .HasForeignKey<Report>(b => b.ReportedUserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Disappearance> Disappearances { get; set; }
        
    }
}