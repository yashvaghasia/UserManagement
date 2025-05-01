using UserManagement.Models;

public interface IHobbyRepository
{
    Task<IEnumerable<Hobby>> GetAllAsync();
    Task<Hobby> GetByIdAsync(int id);
    Task<Hobby> AddAsync(Hobby hobby);
    Task<Hobby> UpdateAsync(Hobby hobby);
    Task<bool> DeleteAsync(int id);
    Task<Hobby> CreateAsync(Hobby hobby);
    Task<Hobby> UpdateAsync(int id, Hobby hobby);

}
