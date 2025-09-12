using CliVet.Data.Context;
using CliVet.DTO;
using CliVet.Model;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Services
{
    public class PetService
    {
        public readonly CliVetContext _context;
        public PetService(CliVetContext context) => _context = context;

        public LerPetDto CriarPet(CriarPetDto petDto)
        {
            // tutor existe?
            var tutor = _context.Tutores.FirstOrDefault(t => t.Id == petDto.TutorId);

            if (tutor == null) throw new ArgumentException($"Tutor com ID {petDto.TutorId} não encontrado.");

            // nome do pet é exclusivo?
            var nomePetExiste = _context.Pets.Any(p => p.Nome.ToLower() == petDto.Nome.ToLower());

            if (nomePetExiste) throw new ArgumentException($"Já existe um pet cadastrado com esse nome");

            var petModel = new PetModel
            {
                Nome = petDto.Nome,
                Especie = petDto.Especie,
                TutorId = petDto.TutorId
            };

            _context.Pets.Add(petModel);
            _context.SaveChanges();

            var lerPetDto = new LerPetDto
            {
                Id = petModel.Id,
                Nome = petModel.Nome,
                Especie = petModel.Especie,
                TutorId = petModel.TutorId,
                TutorNome = tutor.Nome
            };

            return lerPetDto;
        }

        public List<LerPetDto> GetPets()
        {
            var pets = _context.Pets.Include(PetController => PetController.Tutor).ToList();

            var petsDto = pets.Select(pet => new LerPetDto
            {
                Id = pet.Id,
                Nome = pet.Nome,
                Especie = pet.Especie,
                TutorId = pet.TutorId,
                TutorNome = pet.Tutor.Nome
            }).ToList();

            return petsDto;
        }

        public LerPetDto GetPetPorId(int id)
        {
            var pet = _context.Pets.Include(pet => pet.Tutor).FirstOrDefault(pet => pet.Id == id);

            if (pet == null) return null;

            var petDto = new LerPetDto
            {
                Id = pet.Id,
                Nome = pet.Nome,
                Especie = pet.Especie,
                TutorId = pet.TutorId,
                TutorNome = pet.Tutor.Nome
            };

            return petDto;
        }

        public LerPetDto EditarPet(int id, EditarPetDto petDto)
        {
            var pet = _context.Pets.Include(p => p.Tutor).FirstOrDefault(p => p.Id == id);

            if (pet == null) return null;

            var nomeExiste = _context.Pets.Any(p => p.Nome.ToLower() == petDto.Nome.ToLower() && p.Id != id);

            if (nomeExiste) throw new ArgumentException($"Já existe outro pet cadastrado com o nome '{petDto.Nome}'.");

            pet.Nome = petDto.Nome;
            pet.Especie = petDto.Especie;

            _context.SaveChanges();

            return GetPetPorId(id);
        }

        public bool DeletarPet(int id)
        {
            var pet = _context.Pets.Find(id);

            if (pet == null) return false;

            _context.Pets.Remove(pet);

            _context.SaveChanges();

            return true;
        }
    }
}
