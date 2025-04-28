using AutoMapper;
using UserManagement.Models.Dto.Role;
using UserManagement.Models.Entities;

public class UserProfile : Profile
{
    public UserProfile()
    {
        
        CreateMap<UserUpdateDto, User>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) 
            .ForMember(dest => dest.FirstName, opt => {
                opt.DoNotAllowNull();
                opt.MapFrom(src => src.FirstName.Trim());
            })
            .ForMember(dest => dest.LastName, opt => {
                opt.DoNotAllowNull();
                opt.MapFrom(src => src.LastName.Trim());
            })
            .ForMember(dest => dest.Email, opt => {
                opt.DoNotAllowNull();
                opt.MapFrom(src => src.Email.Trim());
            })
            .ForMember(dest => dest.RoleId, opt => opt.DoNotAllowNull())
            ;
       
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();

    }
}
