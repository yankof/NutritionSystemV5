namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluacionesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EvaluacionesController> _logger;

        public EvaluacionesController(IMediator mediator, ILogger<EvaluacionesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvaluacion([FromBody] CreateEvaluacionCommand command)
        {
            _logger.LogInformation("Received CreateEvaluacionCommand for ConsultaId: {ConsultaId}", command.ConsultaId);
            try
            {
                var evaluacionId = await _mediator.Send(command);
                _logger.LogInformation("Evaluacion created successfully with ID: {EvaluacionId}", evaluacionId);
                return CreatedAtAction(nameof(GetEvaluacionById), new { id = evaluacionId }, new { Id = evaluacionId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreateEvaluacion: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreateEvaluacionCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear la evaluación.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvaluacionById(Guid id)
        {
            // Asume que tienes un Query y DTO para Evaluacion
            // var evaluacion = await _mediator.Send(new GetEvaluacionByIdQuery { Id = id });
            // if (evaluacion == null) return NotFound();
            // return Ok(evaluacion);
            return StatusCode(501, "Not Implemented: Implement GetEvaluacionByIdQuery and its handler.");
        }
    }
}