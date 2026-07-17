namespace HRMS.Api.Contracts.Employees;

public sealed record RegisterEmployeeRequest(
    string FullName,
    string Email);