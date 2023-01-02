using Microsoft.EntityFrameworkCore;
using RedeSocial.Domain.Entities;

namespace RedeSocial.Infrastructure.Context {
    public class RedeSocialDbContext : DbContext {

        public RedeSocialDbContext() {
            Database.EnsureCreated();
        }

        public DbSet<Perfil> Perfils_ { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Relacionamento> Relacionamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Comentario>()
                .HasOne(p => p.Post)
                .WithMany(b => b.Comentarios)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Relacionamento>()
                .HasKey(am => new { am.PerfilIdA, am.PerfilIdB });
            modelBuilder.Entity<Relacionamento>()
                .HasOne(am => am.PerfilA)
                .WithMany(a => a.RelacionamentosB)
                .HasForeignKey(ad => ad.PerfilIdA);
            modelBuilder.Entity<Relacionamento>()
                .HasOne(ad => ad.PerfilB)
                .WithMany(d => d.RelacionamentosA)
                .HasForeignKey(ad => ad.PerfilIdB)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RedeSocial;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
