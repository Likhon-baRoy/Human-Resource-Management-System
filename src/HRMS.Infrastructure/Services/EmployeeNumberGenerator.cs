using HRMS.Application.Abstractions.Services;
using HRMS.Domain.ValueObjects;
using HRMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Services;

public sealed class EmployeeNumberGenerator : IEmployeeNumberGenerator
{
    private readonly HRMSDbContext _dbContext;

    public EmployeeNumberGenerator(HRMSDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        _dbContext = dbContext;
    }

    public async Task<EmployeeNumber> GenerateNextAsync(CancellationToken cancellationToken = default)
    {
        var sequence = await _dbContext.Database
            .SqlQuery<long>(
                $"""
                SELECT nextval('hr.employee_number_sequence') AS "Value"
                """)
            .SingleAsync(cancellationToken);

        return EmployeeNumber.FromSequence(sequence);
    }
}