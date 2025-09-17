using CliVet.Domain;
using CliVet.Domain.Repositories;
using CliVet.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Infrastructure.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly CliVetContext _context;

        public TutorRepository(CliVetContext context)
        {
            _context = context;
        }

        public Tutor CriarTutor(Tutor tutor)
        {
            _context.Tutores.Add(tutor);
            _context.SaveChanges();

            return tutor;
        }

        public List<Tutor> GetTutores()
        {
            return _context.Tutores.ToList();
        }

        public Tutor GetTutorPorId(int id)
        {
            return _context.Tutores.SingleOrDefault(t => t.Id == id);
        }

        public void EditarTutor(Tutor tutor)
        {
            _context.Entry(tutor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeletarTutor(Tutor tutor)
        {
            _context.Tutores.Remove(tutor);
            _context.SaveChanges();
        }

        public bool CpfJaExiste(string cpf)
        {
            return _context.Tutores.Any(t => t.Cpf == cpf);
        }
    }
}
