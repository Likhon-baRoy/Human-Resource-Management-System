using HRMS.Application.Employees.Commands.RegisterEmployee;
using Microsoft.Extensions.DependencyInjection;

namespace HRMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<
            ICommandHandler<
                RegisterEmployeeCommand,
                RegisterEmployeeResult>,
            RegisterEmployeeCommandHandler>();

        return services;
    }
}