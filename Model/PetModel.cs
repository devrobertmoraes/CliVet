using System.ComponentModel.DataAnnotations;

namespace CliVet.Model
{
    public class PetModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Especie { get; set; }
        public int TutorId { get; set; }
        public TutorModel Tutor { get; set; }
    }
}
