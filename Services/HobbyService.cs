using AutoMapper;
using UserManagement.Models.Entities;
using UserManagement.Models.Response;

public class HobbyService : IHobbyService
{
    private readonly IHobbyRepository _hobbyRepository;
    private readonly IMapper _mapper;

    public HobbyService(IHobbyRepository hobbyRepository, IMapper mapper)
    {
        _hobbyRepository = hobbyRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<HobbyDto>>> GetAllHobbiesAsync()
    {
        var hobbies = await _hobbyRepository.GetAllAsync();
        return new ApiResponse<List<HobbyDto>>(true, "Fetched successfully", _mapper.Map<List<HobbyDto>>(hobbies));
    }

    public async Task<ApiResponse<HobbyDto>> GetHobbyByIdAsync(int id, HobbyDto hobbyDto)
    {
        var hobby = await _hobbyRepository.GetByIdAsync(id);
        return hobby == null
            ? new ApiResponse<HobbyDto>(false, "Hobby not found", hobbyDto)
            : new ApiResponse<HobbyDto>(true, "Fetched successfully", _mapper.Map<HobbyDto>(hobby));
    }

    public async Task<ApiResponse<HobbyDto>> CreateHobbyAsync(HobbyDto hobbyDto)
    {
        var hobby = _mapper.Map<Hobby>(hobbyDto);
        await _hobbyRepository.AddAsync(hobby);
        return new ApiResponse<HobbyDto>(true, "Created successfully", _mapper.Map<HobbyDto>(hobby));
    }

    public async Task<ApiResponse<HobbyDto>> UpdateHobbyAsync(int id, HobbyDto hobbyDto)
    {
        var hobby = await _hobbyRepository.GetByIdAsync(id);
        if (hobby == null)
            return new ApiResponse<HobbyDto>(false, "Hobby not found", hobbyDto);

        _mapper.Map(hobbyDto, hobby);
        await _hobbyRepository.UpdateAsync(hobby);
        return new ApiResponse<HobbyDto>(true, "Updated successfully", _mapper.Map<HobbyDto>(hobby));
    }

    public async Task<ApiResponse<HobbyDto>> DeleteHobbyAsync(int id)
    {
        var hobby = await _hobbyRepository.GetByIdAsync(id);
        if (hobby == null)
        {
            return new ApiResponse<HobbyDto>(false, "Hobby not found", null);
        }

        await _hobbyRepository.DeleteAsync(id);

        return new ApiResponse<HobbyDto>(true, "Deleted successfully", null);
    }


    public Task<ApiResponse<HobbyDto>> GetHobbyByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
