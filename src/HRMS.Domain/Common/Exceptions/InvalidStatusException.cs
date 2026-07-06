using HRMS.Domain.Common.Exceptions;

public sealed class InvalidStatusException : DomainException
{
    public InvalidStatusException(string message) : base(message)
    {

    }
}