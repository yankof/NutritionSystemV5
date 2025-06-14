using NutritionSystem.Domain.Entities;

namespace NutritionSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PlanesController> _logger;

        public PlanesController(IMediator mediator, ILogger<PlanesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("Crear-Plan")]
        public async Task<IActionResult> CreatePlan([FromBody] CreatePlanCommand command)
        {
            _logger.LogInformation("Received CreatePlanCommand for ConsultaId: {ConsultaId}", command.ConsultaId);
            try
            {
                var planCreado = await _mediator.Send(command);
                _logger.LogInformation("Plan created successfully with ID: {PlanId}", planCreado.Id);
                return Ok(planCreado);
                
                //return CreatedAtAction(nameof(GetPlanById), new { id = planId }, new { Id = planId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Bad request for CreatePlan: {Message}", ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing CreatePlanCommand.");
                return StatusCode(500, "Ocurrió un error interno al crear el plan.");
            }
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetPlanById(Guid id)
        {
            _logger.LogInformation("Attempting to retrieve Consulta with ID: {Id}", id);
            var query = new GetPlanByIdQuery { Id = id };
            var plan = await _mediator.Send(query);
            
            if (plan == null)
            {
                _logger.LogWarning("Consulta with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Consulta found with ID: {Id}", id);
            return Ok(plan);
        }
    }
}