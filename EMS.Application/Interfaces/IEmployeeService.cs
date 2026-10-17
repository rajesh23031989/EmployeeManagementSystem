using EMS.Application.DTOs;

namespace EMS.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();

    Task<EmployeeDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreateEmployeeDto dto);

    Task UpdateAsync(int id, UpdateEmployeeDto dto);

    Task DeleteAsync(int id);
}