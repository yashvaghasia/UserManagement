using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories.Interfaces;

namespace UserManagement.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserDbContext _context;

        public RoleRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.Where(r => r.IsDeleted).ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted);
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await GetRoleByIdAsync(id);
            if (role == null) return false;

            role.IsDeleted = false;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
