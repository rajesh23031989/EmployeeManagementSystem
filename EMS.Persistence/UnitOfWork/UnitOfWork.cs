using EMS.Application.Interfaces;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeDbContext _context;

    public IRepository<Employee> Employees { get; }

    public IRepository<Department> Departments { get; }

    public UnitOfWork(
        EmployeeDbContext context,
        IRepository<Employee> employees,
        IRepository<Department> departments)
    {
        _context = context;

        Employees = employees;

        Departments = departments;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}