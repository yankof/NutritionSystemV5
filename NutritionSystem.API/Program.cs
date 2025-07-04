using NutritionSystem.Application.Features.Diagnostico.Commands;
using NutritionSystem.Application.Features.Evaluacion.Commands;
using NutritionSystem.Application.Features.Plan.Commands;
using NutritionSystem.Domain.Common;
using NutritionSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- Configuración de Logging con Serilog ---
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/nutrition-system-.log", rollingInterval: RollingInterval.Day)
);

// --- Configuración de DbContext ---
// Inyectamos IMediator en el DbContext
builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped // DbContext debe ser Scoped
);


// --- Inyección de Dependencias ---

//// Repositorios específicos
//builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
//builder.Services.AddScoped<INutricionistaRepository, NutricionistaRepository>();
//builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
//builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
//builder.Services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
//builder.Services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
//builder.Services.AddScoped<IPlanRepository, PlanRepository>();
//builder.Services.AddScoped<IHistorialPacienteRepository, HistorialPacienteRepository>();
//builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

//// Unit of Work
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddInfrastructure(builder.Configuration);
// MediatR para comandos, queries Y EVENTOS DE DOMINIO
builder.Services.AddMediatR(cfg =>
{
    // Escanea el ensamblado de la capa de Application para encontrar:
    // - IRequestHandlers (para comandos y queries)
    // - INotificationHandlers (para eventos de dominio)
    cfg.RegisterServicesFromAssembly(typeof(CreatePersonaCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateDiagnosticoCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateEvaluacionCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateNutricionistaCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreatePlanCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DomainEvent).Assembly);


    // Asegúrate de que el ensamblado donde están tus eventos y sus manejadores sea escaneado.
    // En este caso, Features.Nutricionista.Events está en el mismo ensamblado de Application.
});

// Add controllers for API endpoints
//builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();
//builder.Services.AddHostedService<OutboxRelayerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "NutritionSystem API",
        Version = "v1"
    });
});

var app = builder.Build();

// --- Pipeline de la Aplicación ---
app.UseSerilogRequestLogging();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutritionSystem API V1");
        c.RoutePrefix = string.Empty; // Para que Swagger se abra en http://localhost:5096 directamente
    });
//}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();