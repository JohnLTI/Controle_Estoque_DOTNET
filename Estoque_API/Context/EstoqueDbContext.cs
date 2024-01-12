using Estoque_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Estoque_API.Context
{
    public class EstoqueDbContext : DbContext
    {
        public EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : base(options)
        { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

    }
}
