using Architecture.Dto.User;
using Architecture.Entities.Model;
using AutoMapper;

namespace Architecture.BusinessLogic.MapperDto
{
    public class MapUser : Profile
    {
        public MapUser()
        {
            CreateMap<ApplicationUser, UserRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseDto>().ReverseMap();
        }
    }
}