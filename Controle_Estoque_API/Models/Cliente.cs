using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Estoque_API.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        public int Telefone { get; set; }

        [Required]
        public int CPF { get; set; }
    }
}