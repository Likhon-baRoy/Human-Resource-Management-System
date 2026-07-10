using HRMS.Application.Abstractions.Persistence;
using HRMS.Application.Common;
using HRMS.Application.Employees.Errors;
using HRMS.Domain.Employees;
using HRMS.Domain.Services;
using HRMS.Domain.ValueObjects;

namespace HRMS.Application.Employees.Commands.RegisterEmployee;

public sealed class RegisterEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeNumberGenerator _employeeNumberGenerator;

    public RegisterEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IEmployeeNumberGenerator employeeNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(employeeRepository);
        ArgumentNullException.ThrowIfNull(employeeNumberGenerator);

        _employeeRepository = employeeRepository;
        _employeeNumberGenerator = employeeNumberGenerator;
    }

    public async Task<Result<RegisterEmployeeResult>> HandleAsync(
        RegisterEmployeeCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var fullName = new FullName(command.FullName);
        var email = new Email(command.Email);

        if (await _employeeRepository.ExistsByEmailAsync(email, cancellationToken))
        {
            return Result<RegisterEmployeeResult>.Failure(
                EmployeeErrors.EmailAlreadyExists);
        }

        var employeeNumber =
            await _employeeNumberGenerator.GenerateNextAsync(cancellationToken);

        var employee = Employee.Register(
            employeeNumber,
            fullName,
            email);

        await _employeeRepository.AddAsync(
            employee,
            cancellationToken);

        var result = new RegisterEmployeeResult(
            employee.Id,
            employee.EmployeeNumber.Value);

        return Result<RegisterEmployeeResult>.Success(result);
    }
}