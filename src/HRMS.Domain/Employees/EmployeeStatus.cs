namespace HRMS.Domain.Employees;

/// <summary>
/// Represents the current employment status of an employee.
/// </summary>
public enum EmployeeStatus
{
    /// <summary>
    /// Employee has been created but is not yet active.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Employee is currently employed and active.
    /// </summary>
    Active = 2,

    /// <summary>
    /// Employee is still employed but temporarily inactive according to company policy.
    /// </summary>
    Inactive = 3,

    /// <summary>
    /// Employment has ended.
    /// </summary>
    Terminated = 4
}
