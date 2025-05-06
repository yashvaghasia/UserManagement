using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class HobbyController : ControllerBase
{
    private readonly IHobbyService _service;

    public HobbyController(IHobbyService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HobbyDto dto) =>
        Ok(await _service.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] HobbyDto dto) =>
        Ok(await _service.UpdateAsync(id, dto));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) =>
        Ok(await _service.DeleteAsync(id));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) =>
        Ok(await _service.GetByIdAsync(id));

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());
}
