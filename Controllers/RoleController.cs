using Microsoft.AspNetCore.Mvc;
using UserManagement.Models.Response;
using UserManagement.Models.Dto.Role;
using UserManagement.Services.Interfaces;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(new ApiResponse<IEnumerable<RoleDto>>(true, "Roles fetched successfully.", roles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found.", null));
            }

            return Ok(new ApiResponse<RoleDto>(true, "Role fetched successfully.", role));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto roleCreateDto)
        {
            var createdRole = await _roleService.CreateRoleAsync(roleCreateDto);
            return Ok(new ApiResponse<RoleDto>(true, "Role created successfully.", createdRole));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto roleUpdateDto)
        {
            var updatedRole = await _roleService.UpdateRoleAsync(id, roleUpdateDto);
            if (updatedRole == null)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found.", null));
            }

            return Ok(new ApiResponse<RoleDto>(true, "Role updated successfully.", updatedRole));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse<string>(false, "Role not found.", null));
            }

            return Ok(new ApiResponse<string>(true, "Role deleted successfully.", null));
        }
    }
}
