namespace HRMS.Domain.Common.Exceptions;

public sealed class InvalidFullNameException : DomainException
{
    public InvalidFullNameException(string message) : base(message)
    {
        
    }
}
