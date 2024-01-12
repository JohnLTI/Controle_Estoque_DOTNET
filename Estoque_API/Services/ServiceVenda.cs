using Estoque_API.Context;
using Estoque_API.Interfaces;
using Estoque_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace Estoque_API.Services
{
    public class ServiceVenda : IServiceVenda
    {
        private readonly EstoqueDbContext _context;
        private ILogger<ServiceProduto> _logger;
        private bool disposedValue;

        public ServiceVenda(EstoqueDbContext context, ILogger<ServiceProduto> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ServiceVenda() { }

        public ActionResult PostVenda(Venda venda)
        {
            try
            {
                var result = _context.Vendas.Add(venda);
                return result != null ? new OkResult() : new NoContentResult();
            }
            catch(Exception e) 
            {
                return new NoContentResult();
            }            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ServiceVenda()
        // {
        //     // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza no método 'Dispose(bool disposing)'
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
