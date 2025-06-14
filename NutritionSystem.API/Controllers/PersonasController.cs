namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PersonasController> _logger;

        public PersonasController(IMediator mediator, ILogger<PersonasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST: api/Personas
        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] CreatePersonaCommand command)
        {
            _logger.LogInformation("Received CreatePersonaCommand for Nombre: {Nombre}", command.Nombre);
            try
            {
                var personaId = await _mediator.Send(command);
                _logger.LogInformation("Persona created successfully with ID: {PersonaId}", personaId);
                return CreatedAtAction(nameof(GetPersonaById), new { id = personaId }, new { Id = personaId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreatePersona: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreatePersonaCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear la persona.");
            }
        }

        // GET: api/Personas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaById(Guid id)
        {
            _logger.LogInformation("Received GetPersonaByIdQuery for ID: {PersonaId}", id);
            var query = new GetPersonaByIdQuery { Id = id };
            var persona = await _mediator.Send(query);

            if (persona == null)
            {
                _logger.LogInformation("Persona with ID {PersonaId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Persona found: {PersonaId}", id);
            return Ok(persona);
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaDto>>> GetAllPersonas()
        {
            _logger.LogInformation("Received GetAllPersonasQuery.");
            var query = new GetAllPersonasQuery();
            var personas = await _mediator.Send(query);
            _logger.LogInformation("Returned {Count} personas.", personas.Count());
            return Ok(personas);
        }

        // PUT: api/Personas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(Guid id, [FromBody] UpdatePersonaCommand command)
        {
            _logger.LogInformation("Received UpdatePersonaCommand for ID: {PersonaId}", id);
            if (id != command.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                {
                    _logger.LogInformation("Persona with ID {PersonaId} not found for update.", id);
                    return NotFound();
                }
                _logger.LogInformation("Persona with ID {PersonaId} updated successfully.", id);
                return NoContent(); // 204 No Content para una actualización exitosa sin retorno
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for UpdatePersona: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing UpdatePersonaCommand.");
                return StatusCode(500, "Ocurrió un error interno al actualizar la persona.");
            }
        }

        // DELETE: api/Personas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(Guid id)
        {
            _logger.LogInformation("Received DeletePersonaCommand for ID: {PersonaId}", id);
            var command = new DeletePersonaCommand { Id = id };
            try
            {
                var result = await _mediator.Send(command);
                if (!result)
                {
                    _logger.LogInformation("Persona with ID {PersonaId} not found for deletion.", id);
                    return NotFound();
                }
                _logger.LogInformation("Persona with ID {PersonaId} deleted successfully.", id);
                return NoContent(); // 204 No Content para una eliminación exitosa
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing DeletePersonaCommand.");
                return StatusCode(500, "Ocurrió un error interno al eliminar la persona.");
            }
        }
    }
}