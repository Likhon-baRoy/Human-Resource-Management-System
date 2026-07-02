namespace HRMS.Domain.Common.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException(string value) : base($"'{value}' is not a valid email address.")
    {

    }
}
