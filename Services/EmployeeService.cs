using AutoMapper;
using UserManagement.Models.Response;

public class EmployeeService : IEmployeeService

{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<EmployeeDto>>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return new ApiResponse<List<EmployeeDto>>(true, "Fetched successfully", _mapper.Map<List<EmployeeDto>>(employees));
    }

    public async Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id,EmployeeDto employeeDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee == null
            ? new ApiResponse<EmployeeDto>(false, "Employee not found", employeeDto)
            : new ApiResponse<EmployeeDto>(true, "Fetched successfully", _mapper.Map<EmployeeDto>(employee));
    }

    public async Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await _employeeRepository.CreateAsync(employee);
        return new ApiResponse<EmployeeDto>(true, "Created successfully", _mapper.Map<EmployeeDto>(employee));
    }

    public async Task<ApiResponse<EmployeeDto>> UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
            return new ApiResponse<EmployeeDto>(false, "Employee not found", employeeDto);

        _ = _mapper.Map(employee, EmployeeDto);
       await _employeeRepository.UpdateAsync(id,employee);
        return new ApiResponse<EmployeeDto>(true, "Updated successfully", _mapper.Map<EmployeeDto>(employee));
    }

    public async Task<ApiResponse<EmployeeDto>> DeleteEmployeeAsync(int id, EmployeeDto employeeDto)
    {
        var Employee = await _employeeRepository.GetByIdAsync(id);
        if (Employee == null)
            return new ApiResponse<EmployeeDto>(false, "Employee not found", employeeDto);

        await _employeeRepository.DeleteAsync(Employee);
        return new ApiResponse<EmployeeDto>(true, "Deleted successfully", employeeDto);
    }
}
