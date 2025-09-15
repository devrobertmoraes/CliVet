using System.ComponentModel.DataAnnotations;

namespace CliVet.Model
{
    public class TutorModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        public ICollection<PetModel> Pets { get; set; } = [];

        public TutorModel(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }
}
