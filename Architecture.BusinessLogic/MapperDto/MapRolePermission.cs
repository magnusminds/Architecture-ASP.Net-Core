using Architecture.Dto.RolePermission;
using Architecture.Entities.Model;
using AutoMapper;

namespace Architecture.BusinessLogic.MapperDto
{
    public class MapRolePermission : Profile
    {
        public MapRolePermission()
        {
           // CreateMap < ApplicationRole, \RolePermissionResponseDto > ().ReverseMap();
        }
    }
}
