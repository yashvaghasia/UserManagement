using UserManagement.Repositories.Interfaces;
using UserManagement.Services;
using UserManagement.Models.Dto.Role;
using UserManagement.Models.Response;
using UserManagement.Services   ;
using UserManagement.Services.Interfaces;
namespace UserManagement.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponse<List<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            var roleDtos = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return new ApiResponse<List<RoleDto>>(true, "Roles fetched successfully.", roleDtos);
        }

        public async Task<ApiResponse<RoleDto>> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null)
            {
                return new ApiResponse<RoleDto>(false, "Role not found.", null);
            }

            var roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return new ApiResponse<RoleDto>(true, "Role fetched successfully.", roleDto);
        }

        public async Task<ApiResponse<RoleDto>> CreateRoleAsync(RoleDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = true
            };

            await _roleRepository.CreateRoleAsync(role);

            roleDto.Id = role.Id;

            return new ApiResponse<RoleDto>(true, "Role created successfully.", roleDto);
        }

        public async Task<ApiResponse<RoleDto>> UpdateRoleAsync(int id, RoleDto roleDto)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(id);
            if (existingRole == null)
            {
                return new ApiResponse<RoleDto>(false, "Role not found.", null);
            }

            existingRole.Name = roleDto.Name;
            existingRole.UpdatedAt = DateTime.Now;

            await _roleRepository.UpdateRoleAsync(existingRole);

            return new ApiResponse<RoleDto>(true, "Role updated successfully.", roleDto);
        }

        //public async Task<ApiResponse<string>> DeleteRoleAsync(int id)
        //{
        //    var existingRole = await _roleRepository.GetRoleByIdAsync(id);
        //    if (existingRole == null)
        //    {
        //        return new ApiResponse<string>(false, "Role not found.", null);
        //    }

        //    await _roleRepository.DeleteRoleAsync(existingRoleId);

        //    return new ApiResponse<string>(true, "Role deleted successfully.", null);
        //}
    }
}
