using Estoque_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Estoque_API.Context
{
    public class EstoqueDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        // Configuração do MySQL
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("conexao_aqui");*/
    
    }
}
