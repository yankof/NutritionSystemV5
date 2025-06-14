global using MediatR; // Nuevo
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Moq; // Nuevo: para crear un mock de IMediator (necesitarás el paquete NuGet Moq)
global using NutritionSystem.Application.Interfaces;
global using NutritionSystem.Application.Interfaces.Repositories;
global using NutritionSystem.Domain.Common;
global using NutritionSystem.Domain.Entities;
global using NutritionSystem.Infrastructure.Persistence;
global using System.Linq.Expressions;
global using System.Reflection;

global using Joseco.Communication.External.Contracts.Services;
global using NutritionSystem.Integration.EvaluacionNutricional;
global using Joseco.Communication.External.RabbitMQ;
global using Joseco.Communication.External.RabbitMQ.Services;
global using Microsoft.Extensions.DependencyInjection;
global using NutritionSystem.Infrastructure.RabbitMQ.Consumers;

global using NutritionSystem.Application.Abstraction;
global using NutritionSystem.Infrastructure.Observability;

global using NutritionSystem.Integration.Cliente;


