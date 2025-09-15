using CliVet.Data.Context;
using CliVet.DTO;
using CliVet.Model;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Services
{
    public class TutorService
    {
        private readonly CliVetContext _context;
        public TutorService(CliVetContext context) => _context = context;

        public LerTutorDto CriarTutor(CriarTutorDto tutorDto)
        {
            var idade = DateTime.Today.Year - tutorDto.DataNascimento.Year;

            if (idade < 18) throw new Exception("O tutor deve ter 18 anos ou mais");

            var cpfExiste = _context.Tutores.Any(t => t.Cpf == tutorDto.Cpf);

            if (cpfExiste) throw new ArgumentException("O CPF informado já foi cadastrado");

            var tutorModel = tutorDto.ToEntity();

            _context.Tutores.Add(tutorModel);
            _context.SaveChanges();

            var lerTutorDto = new LerTutorDto
            {
                Id = tutorModel.Id,
                Nome = tutorModel.Nome,
                Cpf = tutorModel.Cpf,
                DataNascimento = tutorModel.DataNascimento,
                Idade = idade
            };

            return lerTutorDto;
        }

        public List<LerTutorDto> GetTutores()
        {
            var tutores = _context.Tutores.ToList();

            var tutoresDto = tutores.Select(LerTutorDto.FromEntity).ToList();

            //foreach (var tutor in tutores)
            //{
            //    var idade = DateTime.Today.Year - tutor.DataNascimento.Year;

            //    tutoresDto.Add(new LerTutorDto
            //    {
            //        Id = tutor.Id,
            //        Nome = tutor.Nome,
            //        Cpf = tutor.Cpf,
            //        DataNascimento = tutor.DataNascimento,
            //        Idade = idade
            //    });
            //}

            return tutoresDto;
        }

        public LerTutorDto GetTutorPorId(int id)
        {
            var tutor = _context.Tutores.FirstOrDefault(t => t.Id == id);

            if (tutor == null) return null;

            var idade = DateTime.Today.Year - tutor.DataNascimento.Year;

            var tutorDto = new LerTutorDto
            {
                Id = tutor.Id,
                Nome = tutor.Nome,
                Cpf = tutor.Cpf,
                DataNascimento = tutor.DataNascimento,
                Idade = idade
            };

            return tutorDto;
        }

        public LerTutorDto EditarTutor(int id, EditarTutorDto tutorDto)
        {
            var tutor = _context.Tutores.Find(id);

            if (tutor == null) return null;

            tutor.Nome = tutorDto.Nome;

            _context.SaveChanges();

            return GetTutorPorId(id);
        }

        public bool DeletarTutor(int id)
        {
            var tutor = _context.Tutores.Include(t => t.Pets).FirstOrDefault(t => t.Id == id);

            if (tutor == null) return false;

            // não posso deletar tutor com pets atrelados
            if (tutor.Pets.Any()) throw new InvalidOperationException("Não é possível excluir um tutor que possui pets atrelados");

            _context.Tutores.Remove(tutor);

            _context.SaveChanges();

            return true;
        }
    }
}
