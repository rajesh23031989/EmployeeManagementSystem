using EMS.Application.Interfaces;
using EMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IRepository<Employee> _repository;

    public TestController(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employees = await _repository.GetAllAsync();

        return Ok(employees);
    }
}