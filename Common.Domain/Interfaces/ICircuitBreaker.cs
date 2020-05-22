using Common.Domain.Model;

namespace Common.Domain.Interfaces
{
    public interface ICircuitBreaker
    {
        bool ExecuteException(CircuitBreakerParameters model);
        int CountException(string SecurityProcess);
    }
}
