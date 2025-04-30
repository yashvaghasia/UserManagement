public interface IEmployeeRepository
{
    Task<List<EmployeeResponseDto>> GetAllAsync();
    Task<EmployeeResponseDto> GetByIdAsync(int id);
    Task AddAsync(EmployeeDto dto);
    Task UpdateAsync(int id, EmployeeDto dto);
    Task DeleteAsync(int id);
}
