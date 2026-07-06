namespace HRMS.Domain.Employees;

/// <summary>
/// Represents the current employment status of an employee.
/// </summary>
public enum EmployeeStatus
{
    /// <summary>
    /// Employee has been created but is not yet active.
    /// </summary>
    Pending,

    /// <summary>
    /// Employee is currently employed and active.
    /// </summary>
    Active,

    /// <summary>
    /// Employee is still employed but temporarily inactive according to company policy.
    /// </summary>
    Inactive,

    /// <summary>
    /// Employment has ended.
    /// </summary>
    Terminated
}
