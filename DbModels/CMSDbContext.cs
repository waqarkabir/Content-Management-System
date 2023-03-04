using DbModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DbModels
{
    public class CMSDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public CMSDbContext(DbContextOptions<CMSDbContext> options, IConfiguration Configuration) : base(options)
        {
            configuration = Configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("cmsDB"));
        }

        public DbSet<NewPage> Pages { get; set; }
        public DbSet<PageComponent> Components { get; set; }
        public DbSet<PageImage> Images { get; set; }
        public DbSet<PageStyle> Styles { get; set; }
        public DbSet<PageTemplate> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewPage>()
                .HasMany(p => p.Components)
                .WithOne()
                .HasForeignKey(c => c.PageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NewPage>()
                .HasOne(p => p.Template)
                .WithMany()
                .HasForeignKey(p => p.TemplateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<PageComponent>()
            //    .HasMany(c => c.PageStyles)
            //    .WithOne()
            //    .HasForeignKey(s => s.ComponentId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PageImage>()
                .HasOne(i => i.Page)
                .WithMany()
                .HasForeignKey(i => i.PageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<PageTemplate>()
            //    .HasMany(t => t.Components)
            //    .WithOne()
            //    .HasForeignKey(c => c.TemplateId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
