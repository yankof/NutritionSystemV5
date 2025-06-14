# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="http://+:8080" 
ENV connectionStrings__DefaultConnection="Server=192.168.1.144,1433;Database=PacienteDB;User Id=sa;Password=Fr3ddy*123;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NutritionSystem.API/NutritionSystem.API.csproj", "NutritionSystem.API/"]
COPY ["NutritionSystem.Application/NutritionSystem.Application.csproj", "NutritionSystem.Application/"]
COPY ["NutritionSystem.Domain/NutritionSystem.Domain.csproj", "NutritionSystem.Domain/"]
COPY ["NutritionSystem.Infrastructure/NutritionSystem.Infrastructure.csproj", "NutritionSystem.Infrastructure/"]
COPY ["NutritionSystem.Integration/NutritionSystem.Integration.csproj", "NutritionSystem.Integration/"]
RUN dotnet restore "./NutritionSystem.API/NutritionSystem.API.csproj"
COPY . .
WORKDIR "/src/NutritionSystem.API"
RUN dotnet build "./NutritionSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NutritionSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NutritionSystem.API.dll"]