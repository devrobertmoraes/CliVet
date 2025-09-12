using System.ComponentModel.DataAnnotations;

namespace CliVet.DTO
{
    public class CriarPetDto
    {
        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Especie { get; set; }

        [Required]
        public int TutorId { get; set; }
    }
}
