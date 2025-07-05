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


builder.Services.AddInfrastructure(builder.Configuration, builder.Environment, builder.Environment.ApplicationName);

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