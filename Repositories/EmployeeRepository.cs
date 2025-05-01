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

    

    public async Task DeleteAsync(int id , EmployeeDto employeeDto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return;
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
    }
    public async Task<Employee> CreateAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee> UpdateAsync(int id, Employee employee)
    {
        var existing = await _context.Employees
            .Include(e => e.EmployeeSkills)
            .Include(e => e.EmployeeHobbies)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existing == null)
            return null;

        existing.FirstName = employee.FirstName;
        existing.LastName = employee.LastName;
        existing.Address = employee.Address;
        existing.BirthDate = employee.BirthDate;
        existing.JoiningDate = employee.JoiningDate;
        existing.Gender = employee.Gender;
        existing.RoleId = employee.RoleId;

        // Update Skills & Hobbies
        existing.EmployeeSkills = employee.EmployeeSkills;
        existing.EmployeeHobbies = employee.EmployeeHobbies;

        await _context.SaveChangesAsync();
        return existing;
    }
}
