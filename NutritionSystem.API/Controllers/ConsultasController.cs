namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ConsultasController> _logger;

        public ConsultasController(IMediator mediator, ILogger<ConsultasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsulta([FromBody] CreateConsultaCommand command)
        {
            _logger.LogInformation(
                "Received CreateConsultaCommand for PacienteId: {PacienteId} and NutricionistaId: {NutricionistaId}",
                command.PacienteId, command.NutricionistaId);
            try
            {
                var consultaId = await _mediator.Send(command);
                _logger.LogInformation("Consulta created successfully with ID: {ConsultaId}", consultaId);
                return CreatedAtAction(nameof(GetConsultaById), new { id = consultaId }, new { Id = consultaId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreateConsulta: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreateConsultaCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear la consulta.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultaById(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve Consulta with ID: {Id}", id);
            var consulta = await _mediator.Send(new GetConsultaByIdQuery { Id = id });
            if (consulta == null)
            {
                _logger.LogWarning("Consulta with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Consulta found with ID: {Id}", id);
            return Ok(consulta);
        }

        // Puedes añadir más endpoints aquí:
        // PUT para actualizar
        // DELETE para eliminar
        // GET para obtener todas las consultas o consultas por filtros
    }
}