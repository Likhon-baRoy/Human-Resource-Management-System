namespace HRMS.Application.Abstractions.Persistence;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}