using HRMS.Domain.Common;

namespace HRMS.Application.Common;

public abstract class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        bool hasError = error != Error.None;

        if (isSuccess && hasError)
        {
            throw new InvalidOperationException(
                "A successful result cannot contain an error.");
        }

        if (!isSuccess && !hasError)
        {
            throw new InvalidOperationException(
                "A failed result must contain an error.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }
}

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
                    $"Cannot access the value of a failed result. {Error}");
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