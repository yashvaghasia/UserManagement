using Microsoft.EntityFrameworkCore;

public class HobbyRepository : IHobbyRepository
{
    private readonly UserDbContext _context;

    public HobbyRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hobby>> GetAllAsync()
    {
        return await _context.Hobbies.ToListAsync();
    }

    public async Task<Hobby> GetByIdAsync(int id)
    {
        return await _context.Hobbies.FindAsync(id);
    }

    public async Task<Hobby> AddAsync(Hobby hobby)
    {
        _context.Hobbies.Add(hobby);
        await _context.SaveChangesAsync();
        return hobby;
    }

    public async Task<Hobby> UpdateAsync(Hobby hobby)
    {
        _context.Hobbies.Update(hobby);
        await _context.SaveChangesAsync();
        return hobby;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var hobby = await _context.Hobbies.FindAsync(id);
        if (hobby == null) return false;

        _context.Hobbies.Remove(hobby);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Hobby> CreateAsync(Hobby hobby)
    {
        _context.Hobbies.Add(hobby);
        await _context.SaveChangesAsync();
        return hobby;
    }

    public async Task<Hobby> UpdateAsync(int id, Hobby hobby)
    {
        var existing = await _context.Hobbies.FindAsync(id);
        if (existing == null)
            return null;

        existing.Name = hobby.Name;
        await _context.SaveChangesAsync();
        return existing;
    }
}
