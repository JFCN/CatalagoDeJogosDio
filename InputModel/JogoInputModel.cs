using System;
using System.ComponentModel.DataAnnotations;

namespace Catalago.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="O nome do jogo deve conter entre 3 e 100 caracteres")]//fail fast validation
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da produtora deve conter entre 1 e 100 caracteres")]
        public string Produtora { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no minimo 1 real e no máximo 1.000 reais")]
        public double Preco { get; set; }

    }
}
