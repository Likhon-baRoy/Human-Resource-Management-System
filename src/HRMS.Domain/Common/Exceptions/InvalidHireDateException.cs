using HRMS.Domain.Common.Exceptions;

public sealed class InvalidHireDateException : DomainException
{
    public InvalidHireDateException(string message) : base(message)
    {

    }
}