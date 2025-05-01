namespace UserManagement.Repositories
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetAllAsync();
        Task<Skill> GetByIdAsync(int id);
        Task<Skill> AddAsync(Skill skill);
        Task<Skill> UpdateAsync(Skill skill);
        Task<bool> DeleteAsync(int id);
    }

}
