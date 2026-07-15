using HRMS.Application.Abstractions.Persistence;
using HRMS.Domain.Employees;
using HRMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
     private readonly HRMSDbContext _dbContext;

     public EmployeeRepository(HRMSDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        _dbContext = dbContext;
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(employee);

        await _dbContext.Employees.AddAsync(employee, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Employees.AnyAsync(
            employee => employee.Email == email,
            cancellationToken);
    }
}