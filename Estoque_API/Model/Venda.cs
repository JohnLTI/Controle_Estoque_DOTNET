using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estoque_API.Model
{
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo IdVenda é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor de IdVenda deve ser um número inteiro positivo.")]
        public int IdVenda { get; set; }

        [Required(ErrorMessage = "O campo DataVenda é obrigatório.")]
        public DateTime DataVenda { get; set; }

        [ForeignKey("Produto")]
        [Required(ErrorMessage = "O campo IdProduto é obrigatório.")]
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo QuantidadeItens é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor de QuantidadeItens deve ser um número inteiro positivo.")]
        public int QuantidadeItens { get; set; }

        [Required(ErrorMessage = "O campo PrecoVenda é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor de PrecoVenda deve ser maior que zero.")]
        public decimal PrecoVenda { get; set; }
    }
}