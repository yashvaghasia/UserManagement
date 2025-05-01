using UserManagement.Models.Entities;

public interface ISkillService
{
    Task<IEnumerable<SkillDto>> GetAllAsync();
    Task<SkillDto> GetByIdAsync(int id);
    Task<SkillDto> AddAsync(SkillDto skillDto);
    Task<SkillDto> UpdateAsync(int id, SkillDto skillDto);
    Task<bool> DeleteAsync(int id);
}
