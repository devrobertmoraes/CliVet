using CliVet.Model;

namespace CliVet.DTO
{
    public class LerTutorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        public static LerTutorDto FromEntity(TutorModel tutor)
        {
            return new LerTutorDto
            {
                Id = tutor.Id,
                Nome = tutor.Nome,
                Cpf = tutor.Cpf,
                DataNascimento = tutor.DataNascimento
            };
        }
    }
}
