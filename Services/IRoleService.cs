using UserManagement.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.Entities;
using UserManagement.Models.Dto.Role;

namespace UserManagement.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
        Task<RoleDto> CreateRoleAsync(CreateRoleDto roleCreateDto);
        Task<RoleDto> UpdateRoleAsync(int id, UpdateRoleDto roleUpdateDto);
        Task<bool> DeleteRoleAsync(int id);
    }
}
