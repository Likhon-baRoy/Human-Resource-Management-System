namespace HRMS.Application.Common;

public sealed class Result<T> : Result
    where T : notnull
{
    private readonly T? _value;

    public T Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException(
                    $"Cannot access the value of a failed result. " +
                    $"[{Error.Code}] {Error.Description}");
            }

            return _value!;
        }
    }

    private Result(
        bool isSuccess,
        Error error,
        T? value)
        : base(isSuccess, error)
    {
        if (isSuccess && value is null)
        {
            throw new InvalidOperationException(
                "A successful result must contain a value.");
        }

        _value = value;
    }

    public static Result<T> Success(T value) => new(true, Error.None, value);

    public static Result<T> Failure(Error error) => new(false, error, default!);
}