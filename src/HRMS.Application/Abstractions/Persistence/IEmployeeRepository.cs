using HRMS.Domain.Employees;
using HRMS.Domain.ValueObjects;

namespace HRMS.Application.Abstractions.Persistence;

public interface IEmployeeRepository
{
    Task<bool> ExistsByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Employee employee,
        CancellationToken cancellationToken = default);
}