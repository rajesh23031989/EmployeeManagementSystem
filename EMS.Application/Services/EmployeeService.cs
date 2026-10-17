using EMS.Application.DTOs;
using EMS.Application.Exceptions;
using EMS.Application.Interfaces;
using EMS.Domain.Entities;

namespace EMS.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
    {
        var employees = await _unitOfWork.Employees.GetAllAsync();

        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FullName = $"{e.FirstName} {e.LastName}",
            Email = e.Email,
            Salary = e.Salary,
            Department = ""
        });
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
            return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            FullName = $"{employee.FirstName} {employee.LastName}",
            Email = employee.Email,
            Salary = employee.Salary,
            Department = ""
        };
    }

    public async Task<int> CreateAsync(CreateEmployeeDto dto)
    {
        // Business Rule 1
        if (await _unitOfWork.Employees.ExistsAsync(e => e.Email == dto.Email))
            throw new BadRequestException("Employee email already exists.");

        // Business Rule 2
        if (dto.Salary < 0)
            throw new Exception("Salary cannot be negative.");

        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            Salary = dto.Salary,
            DateOfJoining = dto.DateOfJoining,
            DepartmentId = dto.DepartmentId,
            IsActive = true
        };

        await _unitOfWork.Employees.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync();

        return employee.Id;
    }

    public async Task UpdateAsync(int id, UpdateEmployeeDto dto)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
            throw new NotFoundException("Employee not found.");

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.Phone = dto.Phone;
        employee.Salary = dto.Salary;
        employee.DepartmentId = dto.DepartmentId;

        _unitOfWork.Employees.Update(employee);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(id);

        if (employee == null)
            throw new NotFoundException("Employee not found.");

        _unitOfWork.Employees.Delete(employee);
        await _unitOfWork.SaveChangesAsync();
    }
}