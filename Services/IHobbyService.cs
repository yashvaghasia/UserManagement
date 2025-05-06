using UserManagement.Models.Entities;
using UserManagement.Models.Response;

public interface IHobbyService
{
    Task<ApiResponse<List<HobbyDto>>> GetAllHobbiesAsync();
    Task<ApiResponse<HobbyDto>> GetHobbyByIdAsync(int id);
    Task<ApiResponse<HobbyDto>> CreateHobbyAsync(HobbyDto hobbyDto);
    Task<ApiResponse<HobbyDto>> UpdateHobbyAsync(int id, HobbyDto hobbyDto);
    Task<ApiResponse<HobbyDto>> DeleteHobbyAsync(int id );
}
