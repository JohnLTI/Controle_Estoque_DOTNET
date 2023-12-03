using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Controle_Estoque_API.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        [DisplayName("Preço")]
        [RegularExpression(@"^R$ \d{1,3}(?:.\d{3})*(?:,\d{2})?$")]
        public decimal PrecoUnit { get; set; }
    }
}