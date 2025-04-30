using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly UserDbContext _context;

    public EmployeeRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeResponseDto>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Role)
            .Include(e => e.EmployeeSkills).ThenInclude(es => es.Skill)
            .Include(e => e.EmployeeHobbies).ThenInclude(eh => eh.Hobby)
            .Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                Address = e.Address,
                Gender = e.Gender,
                BirthDate = e.BirthDate,
                JoiningDate = e.JoiningDate,
                RoleName = e.Role.Name,
                Skills = e.EmployeeSkills.Select(s => s.Skill.Name).ToList(),
                Hobbies = e.EmployeeHobbies.Select(h => h.Hobby.Name).ToList()
            }).ToListAsync();
    }

    public async Task<EmployeeResponseDto> GetByIdAsync(int id)
    {
        return await GetAllAsync().ContinueWith(t => t.Result.FirstOrDefault(e => e.Id == id));
    }

    public async Task AddAsync(EmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            BirthDate = dto.BirthDate,
            JoiningDate = dto.JoiningDate,
            Gender = dto.Gender,
            RoleId = dto.RoleId,
            EmployeeSkills = dto.SkillIds.Select(id => new EmployeeSkill { SkillId = id }).ToList(),
            EmployeeHobbies = dto.HobbyIds.Select(id => new EmployeeHobby { HobbyId = id }).ToList()
        };
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, EmployeeDto dto)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeSkills)
            .Include(e => e.EmployeeHobbies)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null) return;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Address = dto.Address;
        employee.BirthDate = dto.BirthDate;
        employee.JoiningDate = dto.JoiningDate;
        employee.Gender = dto.Gender;
        employee.RoleId = dto.RoleId;

        employee.EmployeeSkills = dto.SkillIds.Select(id => new EmployeeSkill { EmployeeId = id, SkillId = id }).ToList();
        employee.EmployeeHobbies = dto.HobbyIds.Select(id => new EmployeeHobby { EmployeeId = id, HobbyId = id }).ToList();

        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return;
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
}
