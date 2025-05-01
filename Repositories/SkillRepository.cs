using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly UserDbContext _context;

    public SkillRepository(UserDbContext context) => _context = context;

    public async Task<IEnumerable<Skill>> GetAllAsync() => await _context.Skills.ToListAsync();

    public async Task<Skill> GetByIdAsync(int id) => await _context.Skills.FindAsync(id);

    public async Task<Skill> AddAsync(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<Skill> UpdateAsync(Skill skill)
    {
        _context.Skills.Update(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var skill = await _context.Skills.FindAsync(id);
        if (skill == null) return false;
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return true;
    }
}
