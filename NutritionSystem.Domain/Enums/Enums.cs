namespace NutritionSystem.Domain.Enums
{
    public enum EstadoActivo
    {
        Activo = 1, // Mapeará a '1' en la BD
        Inactivo = 0 // Mapeará a '0' en la BD
    }

    public enum TipoEvaluacion
    {
        Inicial = 0,
        Seguimiento = 1,
        Final = 2,
    }

    public enum TipoStatus
    {
        Inicial = 0,
        En_Proc = 1,
        Enviado = 2,
        Fallo = 3,
    }

    public enum TipoDiagnostico
    {
        Nutricional = 0,
        Medico = 1,
        General = 2
    }

    public enum TipoPlan
    {
        Dieta = 0,
        Adelgazamiento = 1,
        MasaMuscular = 2,
        Ejercicio = 3,
        Mixto = 4,
    }

    public enum EstatusConsulta
    {
        Activa = 0,
        Finalizada = 1,
        Cancelada = 2,
    }
}