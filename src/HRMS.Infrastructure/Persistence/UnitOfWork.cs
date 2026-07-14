using HRMS.Application.Abstractions.Persistence;

namespace HRMS.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly HRMSDbContext _dbContext;

    public UnitOfWork(HRMSDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}