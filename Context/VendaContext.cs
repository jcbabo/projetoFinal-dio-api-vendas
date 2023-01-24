using Microsoft.EntityFrameworkCore;
using api_vendas.Models;

namespace api_vendas.Context
{
    public class VendaContext : DbContext
    {
        public VendaContext(DbContextOptions<VendaContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Venda>()
                .HasOne(venda => venda.Vendedor)
                .WithOne(vendedor => vendedor.Venda)
                .HasForeignKey<Venda>(venda => venda.VendedorId);

            builder.Entity<Venda>()
                .HasMany(venda => venda.Itens)
                .WithOne(itens => itens.Venda)
                .HasForeignKey(produto => produto.VendaId);
                
        }

        public DbSet<Vendedor>? Vendedor { get; set; }
        public DbSet<Venda>? InfoVendas { get; set; }
        public DbSet<Produto>? Produto { get; set; }
    }
}