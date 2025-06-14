namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PacientesController> _logger;

        public PacientesController(IMediator mediator, ILogger<PacientesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteCommand command)
        {
            _logger.LogInformation("Received CreatePacienteCommand for PersonaId: {PersonaId}", command.PersonaId);
            try
            {
                var pacienteId = await _mediator.Send(command);
                _logger.LogInformation("Paciente created successfully with ID: {PacienteId}", pacienteId);
                return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteId }, new { Id = pacienteId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreatePaciente: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreatePacienteCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear el paciente.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacienteById(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve Paciente with ID: {Id}", id);
            var paciente = await _mediator.Send(new GetPacienteByIdQuery { Id = id });
            if (paciente == null)
            {
                _logger.LogWarning("Paciente with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Paciente found with ID: {Id}", id);
            return Ok(paciente);
        }

        // Puedes añadir más endpoints aquí:
        // PUT para actualizar
        // DELETE para eliminar
        // GET para obtener todos los pacientes o pacientes por filtros
    }
}