using HRMS.Application.Common;
using HRMS.Domain.Employees;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller)
        where T : notnull
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException(
                "Cannot convert a successful result to an error response.");
        }

        if (result.Error == EmployeeErrors.EmailAlreadyExists)
        {
            return controller.Conflict(result.Error);
        }

        if (result.Error == EmployeeErrors.NotFound)
        {
            return controller.NotFound(result.Error);
        }

        return controller.BadRequest(result.Error);
    }
}