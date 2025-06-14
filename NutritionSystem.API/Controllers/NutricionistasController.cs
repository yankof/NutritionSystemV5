namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutricionistasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NutricionistasController> _logger;

        public NutricionistasController(IMediator mediator, ILogger<NutricionistasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST: api/Nutricionistas
        [HttpPost]
        public async Task<IActionResult> CreateNutricionista([FromBody] CreateNutricionistaCommand command)
        {
            _logger.LogInformation("Received CreateNutricionistaCommand for PersonaId: {PersonaId}", command.PersonaId);
            try
            {
                var nutricionistaId = await _mediator.Send(command);
                _logger.LogInformation("Nutricionista created successfully with ID: {NutricionistaId}", nutricionistaId);
                return CreatedAtAction(nameof(GetNutricionistaById), new { id = nutricionistaId }, new { Id = nutricionistaId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreateNutricionista: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreateNutricionistaCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear el nutricionista.");
            }
        }

        // GET: api/Nutricionistas/{id} (Ejemplo simplificado de lectura)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNutricionistaById(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve Nutricionista with ID: {Id}", id);
            var nutricionista = await _mediator.Send(new Application.Features.Nutricionista.Queries.GetNutricionistaByIdQuery { Id = id }); // Asumiendo que crearás este Query
            if (nutricionista == null)
            {
                _logger.LogWarning("Nutricionista with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Nutricionista found with ID: {Id}", id);
            return Ok(nutricionista);
        }
        // ... (Agrega métodos para Update, Delete, GetAll si los necesitas)
    }
}