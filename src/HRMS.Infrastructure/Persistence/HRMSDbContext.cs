using HRMS.Domain.Employees;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Infrastructure.Persistence;

public sealed class HRMSDbContext : DbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public HRMSDbContext(DbContextOptions<HRMSDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(HRMSDbContext).Assembly);
    }
}