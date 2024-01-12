using Estoque_API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Estoque_API.Interfaces
{
    public interface IServiceVenda : System.IDisposable
    {
        ActionResult PostVenda(Venda vendas);
    }
}
