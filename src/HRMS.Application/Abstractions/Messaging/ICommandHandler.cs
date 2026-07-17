using HRMS.Application.Common;

public interface ICommandHandler<TCommand, TResult>
    where TCommand : notnull
    where TResult : notnull
{
    Task<Result<TResult>> HandleAsync(
        TCommand command,
        CancellationToken cancellationToken = default);
}