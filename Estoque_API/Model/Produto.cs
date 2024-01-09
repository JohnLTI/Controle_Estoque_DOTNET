using System.ComponentModel.DataAnnotations;

namespace Estoque_API.Model
{
    public class Produto
    {
        [Key]
        [Required(ErrorMessage = "O campo IdProduto é obrigatório.")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo NomeProduto é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo NomeProduto não pode ter mais de 30 caracteres.")]
        public string NomeProduto { get; set; }

        [StringLength(300, ErrorMessage = "O campo DescricaoProduto não pode ter mais de 300 caracteres.")]
        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "O campo QuantidadeProduto é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor de QuantidadeProduto deve ser um número inteiro positivo.")]
        public int QuantidadeProduto { get; set; }

        [Required(ErrorMessage = "O campo NomeFornecedor é obrigatório.")]
        public string NomeFornecedor { get; set; }

        [Required(ErrorMessage = "O campo PrecoCompra é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de PrecoCompra deve ser maior que zero.")]
        public decimal PrecoCompra { get; set; }

        [Required(ErrorMessage = "O campo PrecoPrevistoVenda é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de PrecoPrevistoVenda deve ser maior que zero.")]
        public decimal PrecoPrevistoVenda { get; set; }

        [Required(ErrorMessage = "O campo ProdutoAVenda é obrigatório.")]
        public bool ProdutoAVenda { get; set; }

        [Required(ErrorMessage = "O campo DataCompra é obrigatório.")]
        public DateTime DataCompra { get; set; }

        [Required(ErrorMessage = "O campo DataAtualizacao é obrigatório.")]
        public DateTime DataAtualizacao { get; set; }

        private string? _dataVencimento;

        [StringLength(15, ErrorMessage = "O campo DataVencimento não pode ter mais de 15 caracteres.")]
        public string? DataVencimento
        {
            
            get => string.IsNullOrEmpty(_dataVencimento) ? "Indeterminado" : _dataVencimento;
            set => _dataVencimento = value ?? "Indeterminado";
        }
    }
}
