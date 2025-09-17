using CliVet.DTO;
using CliVet.Services;
using CliVet.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CliVet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutorController : ControllerBase
    {
        private readonly TutorService _tutorService;
        public TutorController(TutorService tutorService) 
        {
            _tutorService = tutorService;
        }

        [HttpPost]
        public ActionResult CriarTutor([FromBody] CriarTutorDto tutorDto)
        {
            var result = _tutorService.CriarTutor(tutorDto);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetTutorPorId), new { id = result.Data.Id }, result.Data);
            }

            return BadRequest(new {errors = result.Errors});

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
            var result = _tutorService.GetTutorPorId(id);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            switch (result.ErrorType)
            {
                case ErrorType.NotFound:
                    return NotFound(new { message = result.Errors.First() });

                case ErrorType.Validation:
                    return BadRequest(new { errors = result.Errors });

                default:
                    return BadRequest(new { errors = result.Errors });
            }
        }

        [HttpPut("{id}")]
        public ActionResult EditarTutor(int id, [FromBody] EditarTutorDto tutorDto)
        {
            var result = _tutorService.EditarTutor(id, tutorDto);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            switch (result.ErrorType)
            {
                case ErrorType.NotFound:
                    return NotFound(new { message = result.Errors.First() });

                case ErrorType.Validation:
                    return BadRequest(new { errors = result.Errors });

                default:
                    return BadRequest(new { errors = result.Errors });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarTutor(int id)
        {
            var result = _tutorService.DeletarTutor(id);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            switch (result.ErrorType)
            {
                case ErrorType.NotFound:
                    return NotFound(new { message = result.Errors.First() });

                case ErrorType.Validation:
                    return BadRequest(new { errors = result.Errors });

                default:
                    return BadRequest(new { errors = result.Errors });
            }
        }
    }
}
