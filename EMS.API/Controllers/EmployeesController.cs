using EMS.Application.DTOs;
using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeDto dto)
    {
        var id = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateEmployeeDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}