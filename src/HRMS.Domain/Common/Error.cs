namespace HRMS.Domain.Common;

public sealed record Error
{
    public static readonly Error None = new("None", "No error.");
    public string Code { get; }
    public string Description { get; }

    public Error(string code, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Code = code;
        Description = description;
    }

    public override string ToString() => $"[{Code}] {Description}";
}