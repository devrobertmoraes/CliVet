using System.ComponentModel.DataAnnotations;

namespace CliVet.Domain { 
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; }

        [MaxLength(50)]
        public string Especie { get; set; }
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}
