using HRMS.Domain.Common.Exceptions;

namespace HRMS.Domain.ValueObjects;

public sealed record FullName
{
    private const int MinLength = 2;
    private const int MaxLength = 200;

    public string Value { get; }

    public FullName(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = Normalize(value);

        EnsureValidLength(normalized);
        EnsureValidCharacters(normalized);
        EnsureNoRepeatedPunctuation(normalized);

        Value = normalized;
    }

    private static string Normalize(string value)
    {
        var words = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return string.Join(" ", words);
    }

    private static void EnsureValidLength(string value)
    {
        if (value.Length < MinLength)
        {
            throw new InvalidFullNameException(
                $"Full name must be at least {MinLength} characters.");
        }

        if (value.Length > MaxLength)
        {
            throw new InvalidFullNameException(
                $"Full name cannot exceed {MaxLength} characters.");
        }
    }

    private static void EnsureValidCharacters(string value)
    {
        foreach (char c in value)
        {
            if (c == ' ' ||
                char.IsLetter(c) ||
                IsAllowedPunctuation(c))
            {
                continue;
            }

            throw new InvalidFullNameException(
                $"Full name contains an invalid character '{c}'. " +
                "Only letters, spaces, apostrophes ('), hyphens (-), and periods (.) are allowed.");
        }
    }

    private static void EnsureNoRepeatedPunctuation(string value)
    {
        for (int i = 1; i < value.Length; i++)
        {
            if (IsAllowedPunctuation(value[i]) && value[i - 1] == value[i])
            {
                throw new InvalidFullNameException(
                    $"Full name contains invalid repeated punctuation '{value[i]}'. ");
            }
        }
    }

    private static bool IsAllowedPunctuation(char c)
    {
        return c == '\'' || c == '-' || c == '.';
    }
}
