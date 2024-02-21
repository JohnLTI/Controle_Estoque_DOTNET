using Estoque_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Context;
using Estoque_API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Estoque_API.Interfaces;

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
            return produtos == null ? NotFound() : produtos.Count == 0 ? NoContent() : Ok(produtos);

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
    /// Busca o produto pelo nome
    /// </summary>
    /// <param name="nome"></param>
    /// <returns></returns>
    [HttpGet("getproductbyname")]
    public ActionResult<IEnumerable<Produto>> BuscarProdutosPorNome(string nome)
    {
        var produtos = _service.GetByName(nome);
        return produtos;
    }

    /// <summary>
    /// Busca o produto por id unico
    /// </summary>
    /// <param name="id"></param>       
    /// <returns></returns>
    [HttpGet("{id}")]
    public IActionResult BuscarProdutorPorId([FromRoute]int id) 
    {   
        try
        {
            var produtoDB = _service.BuscarProdutoPorId(id);
            return StatusCode(200,produtoDB);
        }
        catch (Exception ex)
        {
            return StatusCode (404,ex.Message);
        }
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
            foreach (var p in produtos)
            {
                StatusCode(200, $"Produto {p.NomeProduto} cadastrado com sucesso");
            }            
        }
    }

    /// <summary>
    /// Retira o produto do estoque em caso de venda.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    [HttpPut("putsellitem/{id}")]
    public IActionResult RetirarProduto([FromRoute]int id, int quantidade)
    {
        try
        {
            var produto = _service.SellItem(id, quantidade);
            var valorVenda = produto.PrecoPrevistoVenda * quantidade;
            return StatusCode(200, $"Vendido {quantidade} {produto.NomeProduto}(s) por R$ {valorVenda.ToString("N2")}");
        }
        catch(Exception ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

}
