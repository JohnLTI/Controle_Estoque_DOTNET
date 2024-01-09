using Estoque_API.Model;
using Estoque_API.Context;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Estoque_API.Services
{
    public interface IServiceProduto
    {
        ActionResult PostProdutos(List<Produto> produtos);
        List<Produto> GetProdutos();
        Produto BuscarProdutoPorId(int id);
        void CloseContext();
    }
    public class ServiceProduto : IServiceProduto
    {
        private readonly EstoqueDbContext _context;
        private ILogger<ServiceProduto> _logger;
      
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
            finally
            {
                _context.Dispose();
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
                                _context.SaveChanges();
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
                            _context.SaveChanges();
                            _context.Dispose();
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
            finally
            {
                _context.Dispose();
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
    }
}
