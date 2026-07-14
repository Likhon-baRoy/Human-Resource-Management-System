using HRMS.Domain.ValueObjects;

namespace HRMS.Domain.Employees;

public sealed class Employee : AuditableEntity
{
    public EmployeeNumber EmployeeNumber { get; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public EmployeeStatus Status { get; private set; }
    public DateOnly? HireDate { get; private set; }

    private Employee(
        EmployeeNumber employeeNumber,
        FullName fullName,
        Email email)
    {
        ArgumentNullException.ThrowIfNull(employeeNumber);
        ArgumentNullException.ThrowIfNull(fullName);
        ArgumentNullException.ThrowIfNull(email);

        Id = Guid.NewGuid();

        EmployeeNumber = employeeNumber;
        FullName = fullName;
        Email = email;
    }

    public static Employee Register(
        EmployeeNumber employeeNumber,
        FullName fullName,
        Email email)
    {
        var employee = new Employee(
            employeeNumber,
            fullName,
            email);

        employee.Status = EmployeeStatus.Pending;

        return employee;
    }

    public void Activate(DateOnly hireDate)
    {
        EnsureCanTransitionToActive();

        EnsureValidHireDate(hireDate);

        Status = EmployeeStatus.Active;

        HireDate = hireDate;
    }

    public void ChangeEmail(Email newEmail)
    {
        ArgumentNullException.ThrowIfNull(newEmail);

        EnsureEmailIsDifferent(newEmail);

        Email = newEmail;
    }

    private void EnsureCanTransitionToActive()
    {
        if (Status != EmployeeStatus.Pending && Status != EmployeeStatus.Inactive)
        {
            throw new InvalidStatusException(
                "Only pending or inactive employees can be activated."
            );
        }
    }

    private void EnsureValidHireDate(DateOnly hireDate)
    {
        if (hireDate > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new InvalidHireDateException(
                "Hire date cannot be in the future."
            );
        }
    }

    private void EnsureEmailIsDifferent(Email email)
    {
        if (Email == email)
        {
            throw new InvalidOperationException(
                "The new email address must be different from the current email address.");
        }
    }
}