using CliVet.Domain;
using System.ComponentModel.DataAnnotations;

namespace CliVet.DTO
{
    public class CriarTutorDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 caracteres.")]
        public string Cpf { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
