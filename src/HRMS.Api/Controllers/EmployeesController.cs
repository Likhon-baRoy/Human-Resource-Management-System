using HRMS.Api.Contracts.Employees;
using HRMS.Api.Extensions;
using HRMS.Application.Common;
using HRMS.Application.Employees.Commands.RegisterEmployee;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers;

[ApiController]
[Route("api/employees")]
public sealed class EmployeesController : ControllerBase
{
    private readonly ICommandHandler<
        RegisterEmployeeCommand,
        RegisterEmployeeResult> _handler;

    public EmployeesController(
        ICommandHandler<
            RegisterEmployeeCommand,
            RegisterEmployeeResult> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        // Manual mapping
        var command = new RegisterEmployeeCommand(
            request.FullName,
            request.Email);

        Result<RegisterEmployeeResult> result =
        await _handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.ToActionResult(this);
        }

        var response = new RegisterEmployeeResponse(
            result.Value.EmployeeId,
            result.Value.EmployeeNumber);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.EmployeeId },
            response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        // Will implement this later.
        return Ok();
    }
}