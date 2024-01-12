using Estoque_API.Model;
using Estoque_API.Context;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Estoque_API.Interfaces;
using Estoque_API.Services;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Estoque_API.Services
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly EstoqueDbContext _context;
        private ILogger<ServiceProduto> _logger;
        private ServiceVenda _serviceVenda = new ServiceVenda();


        public ServiceProduto(EstoqueDbContext context, ILogger<ServiceProduto> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Produto> GetProdutos()
        {
            try
            {
                var produtos =  _context.Produtos.ToList();

                return produtos == null ? null : produtos;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar produtos" + ex.Message);
            }            
            return null;
        }

        public ActionResult PostProdutos(List<Produto> produtos) 
        {
            try
            {
                if (produtos.Count > 0)
                {
                    var todosProdutosCadastrados = GetProdutos();                    

                    if(todosProdutosCadastrados.Count > 0)
                    {
                        foreach (var p in produtos)
                        {                            
                            // Refatorar para os casos especificos 
                            bool produtoExistente = todosProdutosCadastrados.Any(produtos =>
                            produtos.NomeProduto == p.NomeProduto &&
                            produtos.NomeFornecedor == p.NomeFornecedor &&
                            produtos.DataVencimento == p.DataVencimento);

                            if (produtoExistente == false)
                            {
                                _context.Produtos.Add(p);                                
                            }
                            else
                            {
                                _logger.LogInformation($"Produto {p.NomeProduto} já cadastrado no sistema");
                            }
                        }
                    }
                    else
                    {
                        foreach(var p in produtos)
                        {
                            _context.Produtos.Add(p);                           
                        }                        
                    }                 
                    _context.SaveChanges();
                    _logger.LogInformation($"Produtos cadastrados com sucesso");

                    return new OkResult();
                }
                else
                {
                    _logger.LogInformation($"Não há produtos a serem cadastrados.");
                    return new NoContentResult();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar os produtos");
                return new StatusCodeResult(500);
            }
        }

        public void CloseContext()
        {
           this._context.SaveChanges();
           this._context.Dispose();            
        }

        public Produto BuscarProdutoPorId(int id)
        {
            var produtos = _context.Produtos.Find(id);
            if (produtos == null) throw new Exception ("Produto não encontrado");
            return produtos;
        }
        
        public Produto SellItem(int id, int qtd) //Retira a quantidade de itens requisitada durante a venda caso haja em estoque.
        {
                var produtoDb = _context.Produtos.Find(id);

                if (produtoDb == null) throw new Exception("ID não encontrado");
                if (qtd < 0 || qtd > produtoDb.QuantidadeProduto) throw new Exception($"A quantidade requisitada não está disponível no estoque! \nESTOQUE = {produtoDb.QuantidadeProduto} itens");

                produtoDb.QuantidadeProduto = produtoDb.QuantidadeProduto - qtd;
                _context.Produtos.Update(produtoDb);
                RegistrarVenda(PreparaVenda(produtoDb,qtd));
                _context.SaveChanges();

                return produtoDb;
        }

        private void RegistrarVenda(Venda venda)
        {
            using (_serviceVenda)
            {
                _serviceVenda.PostVenda(venda);
            }
        }

        private Venda PreparaVenda(Produto produto, int qtdVendida)
        {
            using (var venda = new Venda())
            {
                venda.ProdutoVendido = produto;
                venda.DataVenda = DateTime.Now;
                venda.QtdItensVendidos = qtdVendida;
                venda.ValorTotalVenda = (produto.PrecoPrevistoVenda * qtdVendida);
                return venda;
            }
            
        }
    }
}
