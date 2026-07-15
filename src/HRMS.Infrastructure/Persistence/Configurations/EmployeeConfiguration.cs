using HRMS.Domain.Employees;
using HRMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMS.Infrastructure.Persistence.Configurations;

public sealed class EmployeeConfiguration
    : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees", "hr");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EmployeeNumber)
            .HasConversion(
                employeeNumber => employeeNumber.Value,
                value => new EmployeeNumber(value))
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.FullName)
            .HasConversion(
                fullName => fullName.Value,
                value => new FullName(value))
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(e => e.EmployeeNumber)
            .IsUnique();

        builder.HasIndex(e => e.Email)
            .IsUnique();
    }
}