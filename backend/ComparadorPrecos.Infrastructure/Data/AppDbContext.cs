using Microsoft.EntityFrameworkCore;
using ComparadorPrecos.Core.Models;

namespace ComparadorPrecos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ListaCompras> ListasCompras { get; set; }
        public DbSet<ItemDesejado> ItensDesejados { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<OpcaoCompra> OpcoesCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações do Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.DataCadastro).HasDefaultValueSql("NOW()");
            });

            // Configurações do ListaCompras
            modelBuilder.Entity<ListaCompras>(entity =>
            {
                entity.HasOne(l => l.Usuario)
                      .WithMany(u => u.ListasCompras)
                      .HasForeignKey(l => l.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(l => l.CriadaEm).HasDefaultValueSql("NOW()");
            });

            // Configurações do ItemDesejado
            modelBuilder.Entity<ItemDesejado>(entity =>
            {
                entity.HasOne(i => i.Usuario)
                      .WithMany(u => u.ItensDesejados)
                      .HasForeignKey(i => i.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.ListaCompras)
                      .WithMany(l => l.ItensDesejados)
                      .HasForeignKey(i => i.ListaComprasId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(i => i.CriadoEm).HasDefaultValueSql("NOW()");
            });

            // Configurações do Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(p => p.CriadoEm).HasDefaultValueSql("NOW()");
                entity.Property(p => p.AtualizadoEm).HasDefaultValueSql("NOW()");
            });

            // Configurações do OpcaoCompra
            modelBuilder.Entity<OpcaoCompra>(entity =>
            {
                entity.HasOne(o => o.ItemDesejado)
                      .WithMany(i => i.OpcoesCompra)
                      .HasForeignKey(o => o.ItemDesejadoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.Produto)
                      .WithMany(p => p.OpcoesCompra)
                      .HasForeignKey(o => o.ProdutoId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(o => o.CriadoEm).HasDefaultValueSql("NOW()");
            });
        }
    }
}