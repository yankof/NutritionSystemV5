using Joseco.DDD.Core.Abstractions;
using Joseco.Outbox.EFCore;
using Nur.Store2025.Observability;
using NutritionSystem.Application;
using NutritionSystem.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

string serviceName = "Nutricion.worker-service";

builder.Services.AddAplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddOutboxBackgroundService<DomainEvent>();

var host = builder.Build();
host.Run();
