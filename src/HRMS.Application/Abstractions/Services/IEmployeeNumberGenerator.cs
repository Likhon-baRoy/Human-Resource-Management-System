using HRMS.Domain.ValueObjects;

namespace HRMS.Application.Abstractions.Services;

public interface IEmployeeNumberGenerator
{
    Task<EmployeeNumber> GenerateNextAsync(
        CancellationToken cancellationToken = default);
}
