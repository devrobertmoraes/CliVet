using CliVet.DTO;
using CliVet.Domain;
using Microsoft.EntityFrameworkCore;
using CliVet.Domain.Repositories;
using CliVet.Shared;

namespace CliVet.Services
{
    public class TutorService
    {
        private readonly ITutorRepository _tutorRepository;
        public TutorService(ITutorRepository tutorRepository)
        {
            _tutorRepository = tutorRepository;
        }

        public OperationResult<LerTutorDto> CriarTutor(CriarTutorDto tutorDto)
        {
            var tutorResult = Tutor.Create(
                tutorDto.Nome,
                tutorDto.Cpf,
                tutorDto.DataNascimento
            );

            if (!tutorResult.IsSuccess)
            {
                return OperationResult<LerTutorDto>.Fail(tutorResult.Errors);
            }

            if (_tutorRepository.CpfJaExiste(tutorDto.Cpf))
            {
                return OperationResult<LerTutorDto>.Fail("O CPF informado já foi cadastrado.");
            }

            var tutorParaSalvar = tutorResult.Data;
            var novoTutor = _tutorRepository.CriarTutor(tutorParaSalvar);

            return OperationResult<LerTutorDto>.Ok(LerTutorDto.FromEntity(novoTutor));
        }

        public List<LerTutorDto> GetTutores()
        {
            var tutores = _tutorRepository.GetTutores();

            var tutoresDto = tutores
                .Select(tutor => LerTutorDto.FromEntity(tutor))
                .ToList();

            return tutoresDto;
        }

        public OperationResult<LerTutorDto> GetTutorPorId(int id)
        {
            var tutor = _tutorRepository.GetTutorPorId(id);

            if (tutor == null) 
                return OperationResult<LerTutorDto>.NotFound($"Tutor com ID {id} não foi encontrado.");

            var tutorDto = LerTutorDto.FromEntity(tutor);
            return OperationResult<LerTutorDto>.Ok(tutorDto);  
        }

        public OperationResult<bool> EditarTutor(int id, EditarTutorDto tutorDto)
        {
            var tutor = _tutorRepository.GetTutorPorId(id);

            if (tutor == null)
            {
                return OperationResult<bool>.NotFound($"Tutor com ID {id} não foi encontrado para edição.");
            }

            tutor.AtualizarNome(tutorDto.Nome);

            _tutorRepository.EditarTutor(tutor);

            return OperationResult<bool>.Ok(true);
        }

        public OperationResult<bool> DeletarTutor(int id)
        {
            var tutor = _tutorRepository.GetTutorPorId(id);

            if (tutor == null)
            {
                return OperationResult<bool>.NotFound($"Tutor com ID {id} não foi encontrado para exclusão.");
            }

            _tutorRepository.DeletarTutor(tutor);

            return OperationResult<bool>.Ok(true);
        }
    }
}
