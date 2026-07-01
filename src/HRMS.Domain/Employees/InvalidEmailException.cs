using HRMS.Domain.Common.Exceptions;

namespace HRMS.Domain.Employees;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException(string value) : base($"'{value}' is not a valid email address.")
    {

    }
}
