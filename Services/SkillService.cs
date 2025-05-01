using AutoMapper;
using UserManagement.Models.Entities;
using UserManagement.Repositories;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _repository;
    private readonly IMapper _mapper;

    public SkillService(ISkillRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SkillDto>> GetAllAsync()
    {
        var skills = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<SkillDto>>(skills);
    }

    public async Task<SkillDto> GetByIdAsync(int id)
    {
        var skill = await _repository.GetByIdAsync(id);
        return _mapper.Map<SkillDto>(skill);
    }

    public async Task<SkillDto> AddAsync(SkillDto skillDto)
    {
        var skill = _mapper.Map<Skill>(skillDto);
        var result = await _repository.AddAsync(skill);
        return _mapper.Map<SkillDto>(result);
    }

    public async Task<SkillDto> UpdateAsync(int id, SkillDto skillDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;

        _mapper.Map(skillDto, existing);
        var result = await _repository.UpdateAsync(existing);
        return _mapper.Map<SkillDto>(result);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
