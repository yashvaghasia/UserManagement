using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Entities;
using UserManagement.Models.Response;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _service;

    public SkillController(ISkillService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(new ApiResponse<IEnumerable<SkillDto>>(true, "Fetched successfully", await _service.GetAllAsync()));

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) =>
        Ok(new ApiResponse<SkillDto>(true, "Fetched successfully", await _service.GetByIdAsync(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SkillDto dto) =>
        Ok(new ApiResponse<SkillDto>(true, "Created", await _service.AddAsync(dto)));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SkillDto dto) =>
        Ok(new ApiResponse<SkillDto>(true, "Updated", await _service.UpdateAsync(id, dto)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(new ApiResponse<bool>(true, "Deleted", await _service.DeleteAsync(id)));
}
