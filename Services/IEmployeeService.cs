using UserManagement.Models.Response;

public interface IEmployeeService
{
    Task<ApiResponse<List<EmployeeDto>>> GetAllEmployeesAsync();
    Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
    Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(EmployeeDto employeeDto);
    Task<ApiResponse<EmployeeDto>> UpdateEmployeeAsync(int id, EmployeeDto employeeDto);
    Task<ApiResponse<EmployeeDto>> DeleteEmployeeAsync(int id);
    
}
