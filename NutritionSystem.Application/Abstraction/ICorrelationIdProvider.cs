namespace NutritionSystem.Application.Abstraction;
public interface ICorrelationIdProvider
{
    string GetCorrelationId();

    void SetCorrelationId(string correlationId);
}
