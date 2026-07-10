namespace HRMS.Domain.Common.Exceptions;

public sealed class InvalidEmployeeNumberException : DomainException
{
    public InvalidEmployeeNumberException(string message) : base(message)
    {

    }
}
