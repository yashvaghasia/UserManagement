public interface IEmployeeRepository
{
    Task<List<EmployeeResponseDto>> GetAllAsync();
    Task<EmployeeResponseDto> GetByIdAsync(int id);
    Task AddAsync(EmployeeDto dto);
    Task<Employee> CreateAsync(Employee employee);
    Task DeleteAsync(int id, EmployeeDto employeeDto);
    Task<Employee> UpdateAsync(int id, Employee employee);



}
