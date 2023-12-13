namespace Estoque_API.Model;

public class Produto
{
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public string DescricaoProduto { get; set; }
    public int QuantidadeProduto { get; set; }
    public string NomeFornecedor { get; set; }
    public decimal PrecoCompra { get; set; }
    public decimal PrecoPrevistoVenda { get; set; }
    public bool ProdutoAVenda { get; set; }
    public DateTime DataCompra { get; set; }
    public DateTime DataAtualizacao { get; set; }
}