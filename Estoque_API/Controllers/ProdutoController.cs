using Estoque_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Context;

namespace Estoque_API.Controllers;

[Route("api/product")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private ILogger<ProdutoController> _logger;
    private readonly EstoqueDbContext _context;
    public ProdutoController(ILogger<ProdutoController> logger, EstoqueDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// Retorna o status da API
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("getstatus")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult GetStatus()
    {
        return Ok();
    }

    /// <summary>
    /// Busca todos os produtos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("getprodutos")]
    public async Task<ActionResult<IEnumerable<Produto>>> BuscarTodosOsProdutos()
    {
        return Ok(new Produto { });
    }

    /// <summary>
    /// Busca o produto por id unico
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "Get")]
    public string BuscarProdutorPorId(int id)
    {
        return "value";
    }

   
    /// <summary>
    /// Cadastra varios produtos novos caso não estejam cadastrados
    /// </summary>
    /// <param name="produto"></param>
    [HttpPost]
    [Route("postseveralproducts")]
    public ActionResult<IEnumerable<Produto>> CadastrarVariosProdutos(List<Produto> produtos)
    {
        try
        {
            if (produtos.Count > 0)
            {
                var todosProdutosCadastrados = _context.Produtos.ToList();
                if (todosProdutosCadastrados.Count != 0)
                {
                    foreach (var p in produtos)
                    {
                        bool produtoExistente = todosProdutosCadastrados.Any(produtos => produtos == p);

                        if (!produtoExistente)
                        {
                            _context.Produtos.Add(p);
                            _context.SaveChanges();
                        }
                    }
                }
                else
                {
                    foreach (var pd in produtos)
                    {
                        _context.Produtos.Add(pd);
                        _context.SaveChanges();
                    }
                }
                _logger.LogInformation($"POST /Produtos cadastrados com sucesso");
                _context.SaveChanges();
            }
            else
            {
                _logger.LogInformation($"POST / Não há produtos à serem cadastrados.");
                return NoContent();
            }
            return CreatedAtAction(nameof(BuscarTodosOsProdutos), new { produtos }, produtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao adicionar os produtos");
            return StatusCode(500);
        }
        finally { _context.SaveChanges(); }
    }

    /*
    /// <summary>
    /// Atualiza as informações do produto.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    [HttpPut("{id}")]
    public void AlterarProduto(int id, [FromBody] Produto produto)
    {
    }

    /// <summary>
    /// Retira o produto do estoque em caso de venda.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    [HttpPut("RetirarProduto/{id}")]
    public void RetirarProduto(int id, [FromBody] Produto produto)
    {
    }*/

}


