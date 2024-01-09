using Estoque_API.Model;
using Estoque_API.Context;
using Microsoft.EntityFrameworkCore;
using Estoque_API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Estoque_API.Interfaces
{
    public interface IServiceProduto
    {
        ActionResult PostProdutos(List<Produto> produtos);
        List<Produto> GetProdutos();
        Produto BuscarProdutoPorId(int id);
        Produto SellItem(int id, int qtd);
        //void CloseContext();
    }
}
