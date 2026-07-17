using HRMS.Domain.Common;

namespace HRMS.Domain.Employees;

public static class EmployeeErrors
{
    public static readonly Error EmailAlreadyExists = new(
        "Employee.EmailAlreadyExists",
        "An employee with this email already exists.");

    public static readonly Error NotFound = new(
        "Employee.NotFound",
        "The employee was not found.");

    public static readonly Error AlreadyActive = new(
        "Employee.AlreadyActive",
        "The employee is already active.");

    public static readonly Error AlreadyInactive = new(
        "Employee.AlreadyInactive",
        "The employee is already inactive.");
}