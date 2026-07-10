using System.Net.Mail;
using HRMS.Domain.Common.Exceptions;

namespace HRMS.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = Normalize(value);

        if (!IsValidFormat(normalized))
        {
            throw new InvalidEmailException(value);
        }

        Value = normalized;
    }

    private static bool IsValidFormat(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static string Normalize(string value)
    {
        return value.Trim().ToLowerInvariant();
    }

    public override string ToString() => Value;
}
