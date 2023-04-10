using Microsoft.EntityFrameworkCore;
using Restaurante.Cheng.Domain.Entities;

namespace Restaurante.Cheng.Data.Context;

public class RestauranteDbContext : DbContext
{
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Garcom> Garcons { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }
    
    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId);

        modelBuilder.Entity<Atendimento>()
            .HasOne(a => a.Mesa)
            .WithMany()
            .HasForeignKey(a => a.MesaId);

        modelBuilder.Entity<Atendimento>()
            .HasOne(a => a.Garcom)
            .WithMany()
            .HasForeignKey(a => a.GarcomId);

        modelBuilder.Entity<Atendimento>()
            .HasMany(a => a.Produtos)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "AtendimentoProduto",
                j => j.HasOne<Produto>().WithMany().HasForeignKey("ProdutoId"),
                j => j.HasOne<Atendimento>().WithMany().HasForeignKey("AtendimentoId")
            );
    }
}