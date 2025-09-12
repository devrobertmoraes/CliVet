using CliVet.DTO;
using CliVet.Services;
using Microsoft.AspNetCore.Mvc;

namespace CliVet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly PetService _petService;

        public PetController(PetService petService) => _petService = petService;

        [HttpPost]
        public ActionResult CriarPet([FromBody] CriarPetDto petDto)
        {
            try
            {
                var novoPet = _petService.CriarPet(petDto);

                return CreatedAtAction(nameof(GetPetPorId), new { id = novoPet.Id }, novoPet);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetPets()
        {
            var pets = _petService.GetPets();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public ActionResult GetPetPorId(int id)
        {
            var pet = _petService.GetPetPorId(id);

            if (pet == null) return NotFound(new { mensagem = $"Pet com ID {id} não encontrado." });

            return Ok(pet);
        }

        [HttpPut("{id}")]
        public ActionResult EditarPet(int id, [FromBody] EditarPetDto petDto)
        {
            try
            {
                var petAtualizado = _petService.EditarPet(id, petDto);

                if (petAtualizado == null) return NotFound(new { mensagem = $"Pet com ID {id} não encontrado." });

                return Ok(petAtualizado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarPet(int id)
        {
            var sucesso = _petService.DeletarPet(id);

            if (!sucesso) return NotFound(new { mensagem = $"Pet com ID {id} não encontrado." });

            return NoContent();
        }
    }

}
