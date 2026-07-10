using HRMS.Domain.Common.Exceptions;

namespace HRMS.Domain.ValueObjects;

public sealed record EmployeeNumber
{
    private const string Prefix = "EMP-";
    private const int NumericLength = 6;

    public string Value { get; }

    public EmployeeNumber(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = Normalize(value);

        EnsureValidPrefix(normalized);
        EnsureNumericPart(normalized);
        EnsureValidLength(normalized);

        Value = normalized;
    }

    private static string Normalize(string value)
        => value.Trim().ToUpperInvariant();

    private static void EnsureValidPrefix(string value)
    {
        if (!value.StartsWith(Prefix))
        {
            throw new InvalidEmployeeNumberException(
                $"Employee number must start with '{Prefix}'.");
        }
    }

    private static void EnsureValidLength(string value)
    {
        if (value.Length != Prefix.Length + NumericLength)
        {
            throw new InvalidEmployeeNumberException(
                $"Employee number must be in the format '{Prefix}000001'.");
        }
    }

    private static void EnsureNumericPart(string value)
    {
        var numericPart = value[Prefix.Length..];

        if (!numericPart.All(char.IsDigit))
        {
            throw new InvalidEmployeeNumberException(
                $"Employee number must contain exactly six digits after '{Prefix}'.");
        }
    }

    public override string ToString() => Value;
}
