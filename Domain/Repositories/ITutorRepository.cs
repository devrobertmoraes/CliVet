namespace CliVet.Domain.Repositories
{
    public interface ITutorRepository
    {
        public Tutor CriarTutor(Tutor tutor);
        public List<Tutor> GetTutores();
        public Tutor GetTutorPorId(int id);
        public void EditarTutor(Tutor tutor);
        public void DeletarTutor(Tutor tutor);
        bool CpfJaExiste(string cpf);
    }
}
