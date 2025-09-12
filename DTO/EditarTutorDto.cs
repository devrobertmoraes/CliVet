using System.ComponentModel.DataAnnotations;

namespace CliVet.DTO
{
    public class EditarTutorDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
    }
}
