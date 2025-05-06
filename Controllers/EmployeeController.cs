using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Response;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repo;

    public EmployeeController(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _repo.GetAllAsync();
        return Ok(new ApiResponse<List<EmployeeResponseDto>>(true, "Employees fetched", result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _repo.GetByIdAsync(id);
        if (result == null)
            return NotFound(new ApiResponse<string>(false, "Employee not found", null));

        return Ok(new ApiResponse<EmployeeResponseDto>(true, "Employee found", result));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeDto dto)
    {
        await _repo.AddAsync(dto);
        return Ok(new ApiResponse<string>(true, "Employee created successfully", null));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Employee employee)
    {
        await _repo.UpdateAsync(id,employee);
        return Ok(new ApiResponse<string>(true, "Employee updated successfully", null));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id,EmployeeDto employeeDto )
    {
        await _repo.DeleteAsync(id, employeeDto);
        return Ok(new ApiResponse<string>(true, "Employee deleted successfully", null));
    }
}
