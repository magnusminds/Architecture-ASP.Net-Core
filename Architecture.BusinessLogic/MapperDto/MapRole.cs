using Architecture.Dto.Role;
using Architecture.Entities.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;


namespace Architecture.BusinessLogic.MapperDto
{
    public class MapRole : Profile
    {
        public MapRole()
        {
            CreateMap<ApplicationRole, RoleRequestDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleResponseDto>().ReverseMap();
            CreateMap<IdentityResult, RoleRequestDto>().ReverseMap();
        }
    }
}
