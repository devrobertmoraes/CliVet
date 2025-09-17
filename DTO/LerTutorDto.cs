using CliVet.Domain;

namespace CliVet.DTO
{
    public class LerTutorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        public static LerTutorDto FromEntity(Tutor tutor)
        {
            var hoje = DateTime.Now;
            var idade = hoje.Year - tutor.DataNascimento.Year;

            if (tutor.DataNascimento.Date > hoje.AddYears(-idade))
            {
                idade--;
            }

            return new LerTutorDto
            {
                Id = tutor.Id,
                Nome = tutor.Nome,
                Cpf = tutor.Cpf,
                Idade = idade
            };
        }
    }
}
