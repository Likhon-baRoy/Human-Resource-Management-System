using System.Net.Mail;
using HRMS.Domain.Common.Exceptions;

namespace HRMS.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = value.Trim().ToLowerInvariant();

        if (!IsValid(normalized))
        {
            throw new InvalidEmailException(value);
        }

        Value = normalized;
    }

    private static bool IsValid(string email)
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

    }

    public override string ToString() => Value;
}
