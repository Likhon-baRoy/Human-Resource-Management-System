namespace HRMS.Api.Contracts.Employees;

public sealed record RegisterEmployeeResponse(
    Guid EmployeeId,
    string EmployeeNumber);