using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Infrastructure.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<CarreiraEntity> Carreiras { get; set; } = null!;
        public DbSet<RoadMapEntity> RoadMaps { get; set; } = null!;
        public DbSet<RoadMapTecnologiaEntity> RoadMapTecnologias { get; set; } = null!;
        public DbSet<TecnologiaEntity> Tecnologias { get; set; } = null!;
        public DbSet<CategoriaEntity> Categorias { get; set; } = null!;
        public DbSet<UsuarioEntity> Usuarios { get; set; } = null!;
        public DbSet<FeedEntity> Feeds { get; set; } = null!;
        public DbSet<FeedItemEntity> FeedItems { get; set; } = null!;
        public DbSet<LocalizacaoEntity> Enderecos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarreiraEntity>()
                .HasOne(c => c.RoadMap)
                .WithOne(r => r.Carreira)
                .HasForeignKey<RoadMapEntity>(r => r.IdCarreira)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarreiraEntity>()
                .HasOne(c => c.Categoria)
                .WithMany(cat => cat.Carreiras)
                .HasForeignKey(c => c.IdCategoria)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RoadMapTecnologiaEntity>()
                .HasKey(rt => new { rt.IdRoadMap, rt.IdTecnologia });

            modelBuilder.Entity<RoadMapTecnologiaEntity>()
                .HasOne(rt => rt.RoadMap)
                .WithMany(r => r.Tecnologias)
                .HasForeignKey(rt => rt.IdRoadMap)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoadMapTecnologiaEntity>()
                .HasOne(rt => rt.Tecnologia)
                .WithMany()
                .HasForeignKey(rt => rt.IdTecnologia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoadMapTecnologiaEntity>()
                .HasIndex(rt => new { rt.IdRoadMap, rt.StepOrder })
                .IsUnique(false);

            modelBuilder.Entity<FeedEntity>()
                .HasKey(f => f.IdUsuario);

            modelBuilder.Entity<FeedEntity>()
                .HasOne(f => f.Usuario)
                .WithOne(u => u.Feed)
                .HasForeignKey<FeedEntity>(f => f.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioEntity>()
                .HasOne(u => u.Localizacao)
                .WithOne()
                .HasForeignKey<UsuarioEntity>(u => u.IdEndereco)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<FeedItemEntity>()
                .HasOne(fi => fi.Feed)
                .WithMany(f => f.Items)
                .HasForeignKey(fi => fi.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeedItemEntity>()
                .HasOne(fi => fi.Tecnologia)
                .WithMany(t => t.FeedItems)
                .HasForeignKey(fi => fi.IdTecnologia)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RoadMapEntity>()
                .HasKey(r => r.IdCarreira);

            modelBuilder.Entity<TecnologiaEntity>()
                .HasKey(t => t.IdTecnologia);

            modelBuilder.Entity<CarreiraEntity>()
                .HasKey(c => c.IdCarreira);

            modelBuilder.Entity<CategoriaEntity>()
                .HasKey(cat => cat.IdCategoria);

            modelBuilder.Entity<UsuarioEntity>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<LocalizacaoEntity>()
                .HasKey(e => e.IdEndereco);
        }
    }
}
