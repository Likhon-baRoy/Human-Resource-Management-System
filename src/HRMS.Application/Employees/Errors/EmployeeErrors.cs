using HRMS.Application.Common;

namespace HRMS.Application.Employees.Errors;

public static class EmployeeErrors
{
    public static readonly Error EmailAlreadyExists = new(
        "Employee.EmailAlreadyExists",
        "An employee with this email address already exists.");
}