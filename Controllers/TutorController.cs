using CliVet.Data.Context;
using CliVet.DTO;
using CliVet.Model;
using CliVet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CliVet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly TutorService _tutorService;
        public TutorController(TutorService tutorService) => _tutorService = tutorService;

        [HttpPost]
        public ActionResult CriarTutor([FromBody] CriarTutorDto tutorDto)
        {
            try
            {
                var novoTutor = _tutorService.CriarTutor(tutorDto);

                return CreatedAtAction(nameof(GetTutorPorId), new {id = novoTutor.Id}, novoTutor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {mensagem = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetTutores()
        {
            var tutores = _tutorService.GetTutores();
            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public ActionResult GetTutorPorId(int id)
        {
            var tutor = _tutorService.GetTutorPorId(id);

            if (tutor == null) return NotFound(new { mensagem = $"Tutor com {id} não encontrado"});

            return Ok(tutor);
        }

        [HttpPut("{id}")]
        public ActionResult EditarTutor(int id, [FromBody] EditarTutorDto tutorDto)
        {
            try
            {
                var tutorAtualizado = _tutorService.EditarTutor(id, tutorDto);

                if (tutorAtualizado == null) return NotFound(new { mensagem = $"Tutor com ID {id} não encontrado." });

                return Ok(tutorAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarTutor(int id)
        {
            try
            {
                var sucesso = _tutorService.DeletarTutor(id);

                if (!sucesso) return NotFound(new { mensagem = $"Tutor com ID {id} não encontrado." });

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno no servidor.", detalhe = ex.Message });
            }
        }
    }
}
