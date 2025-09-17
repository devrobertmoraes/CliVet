using CliVet.Shared;

namespace CliVet.Domain
{
    public class Tutor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public List<Pet> Pets { get; private set; } = [];

        public Tutor() { }

        private Tutor(string nome, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public static OperationResult<Tutor> Create(string nome, string cpf, DateTime dataNascimento)
        {
            var errors = new List<string>();

            // As mesmas validações de antes
            if (string.IsNullOrWhiteSpace(nome)) 
                errors.Add("O campo nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cpf)) 
                errors.Add("O campo cpf é obrigatório.");

            if (cpf.Length != 11) 
                errors.Add("O CPF deve ter exatamente 11 caracteres.");

            if (dataNascimento > DateTime.UtcNow.AddYears(-18)) 
                errors.Add("O usuário deve ter ao menos 18 anos.");

            
            if (errors.Any())
            {
                return OperationResult<Tutor>.Fail(errors);
            }

            var tutor = new Tutor(nome, cpf, dataNascimento);

            return OperationResult<Tutor>.Ok(tutor);
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(novoNome));
            }
            if (novoNome.Length > 100)
            {
                throw new ArgumentException("O nome não pode exceder 100 caracteres.", nameof(novoNome));
            }

            Nome = novoNome;
        }
    }
}
