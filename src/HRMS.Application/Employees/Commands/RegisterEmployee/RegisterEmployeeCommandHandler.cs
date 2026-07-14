using HRMS.Application.Abstractions.Persistence;
using HRMS.Application.Abstractions.Services;
using HRMS.Application.Common;
using HRMS.Application.Employees.Errors;
using HRMS.Domain.Employees;
using HRMS.Domain.ValueObjects;

namespace HRMS.Application.Employees.Commands.RegisterEmployee;

public sealed class RegisterEmployeeCommandHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeNumberGenerator _employeeNumberGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IEmployeeNumberGenerator employeeNumberGenerator,
        IUnitOfWork unitOfWork)
    {
        ArgumentNullException.ThrowIfNull(employeeRepository);
        ArgumentNullException.ThrowIfNull(employeeNumberGenerator);
        ArgumentNullException.ThrowIfNull(unitOfWork);

        _employeeRepository = employeeRepository;
        _employeeNumberGenerator = employeeNumberGenerator;
        _unitOfWork = unitOfWork;
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new RegisterEmployeeResult(
            employee.Id,
            employee.EmployeeNumber.Value);

        return Result<RegisterEmployeeResult>.Success(result);
    }
}