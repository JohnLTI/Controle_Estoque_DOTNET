using Estoque_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Context;
using Estoque_API.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Estoque_API.Controllers;

[Route("api/product")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private ILogger<ProdutoController> _logger;
    private readonly EstoqueDbContext _context;
    private readonly IServiceProduto _service;
   
    public ProdutoController(ILogger<ProdutoController> logger, EstoqueDbContext context, IServiceProduto service)
    {
        _logger = logger;
        _context = context;
        _service = service;
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
    public  ActionResult<IEnumerable<Produto>> BuscarTodosOsProdutos()
    {
        try
        {
            var produtos = _service.GetProdutos();
            return produtos.Count == 0 ? NoContent() : Ok(produtos);

        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao buscar produtos" + ex.Message);
        }
        finally
        {
            _context.Dispose();
        }
        return NoContent();
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
                _service.PostProdutos(produtos);
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
        finally
        {
           // _service.CloseContext();
        }
    }


    /*
    /// <summary>
    /// Atualiza as informações do produto.
    /// Quando o usuario pesquisar qual produto deseja editar
    /// usaremos o metodo de buscar pelo nome e utilizaremos aqui. 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    [HttpPut("{nome}")]
    public ActionResult AlterarProduto(string nome)
    {
        return NotFound();
    }

    
   
    /// <summary>
    /// Retira o produto do estoque em caso de venda.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    [HttpPut("RetirarProduto/{id}")]
    public void RetirarProduto(int id, [FromBody] Produto produto)
    {
    }

    /// <summary>
    /// Busca o produto por id unico
    /// </summary>
    /// <param name="nome"></param>
    /// <returns></returns>
    [HttpGet("{nome}", Name = "Get")]
    public string BuscarProdutosPorNome(string nome)
    {
        return "value";
    }
    */
}
