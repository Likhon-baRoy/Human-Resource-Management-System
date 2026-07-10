using HRMS.Domain.ValueObjects;

namespace HRMS.Domain.Services;

public interface IEmployeeNumberGenerator
{
    Task<EmployeeNumber> GenerateNextAsync(
        CancellationToken cancellationToken = default);
}
