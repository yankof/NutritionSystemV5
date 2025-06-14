namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DiagnosticosController> _logger;

        public DiagnosticosController(IMediator mediator, ILogger<DiagnosticosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiagnostico([FromBody] CreateDiagnosticoCommand command)
        {
            _logger.LogInformation("Received CreateDiagnosticoCommand for ConsultaId: {ConsultaId}", command.ConsultaId);
            try
            {
                var diagnosticoId = await _mediator.Send(command);
                _logger.LogInformation("Diagnostico created successfully with ID: {DiagnosticoId}", diagnosticoId);
                return CreatedAtAction(nameof(GetDiagnosticoById), new { id = diagnosticoId }, new { Id = diagnosticoId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreateDiagnostico: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreateDiagnosticoCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear el diagnóstico.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosticoById(Guid id)
        {
            // Asume que tienes un Query y DTO para Diagnostico
            // var diagnostico = await _mediator.Send(new GetDiagnosticoByIdQuery { Id = id });
            // if (diagnostico == null) return NotFound();
            // return Ok(diagnostico);
            return StatusCode(501, "Not Implemented: Implement GetDiagnosticoByIdQuery and its handler.");
        }
    }
}