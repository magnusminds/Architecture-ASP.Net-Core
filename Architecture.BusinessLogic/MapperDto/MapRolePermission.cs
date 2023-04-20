using Architecture.Dto.RolePermission;
using Architecture.Entities.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.MapperDto
{
    public class MapRolePermission : Profile
    {
        public MapRolePermission()
        {
            CreateMap<ApplicationRole, RolePermissionResponseDto>().ReverseMap();
            CreateMap<ApplicationRole, RolePermissionResponseDto>().ReverseMap();
        }
    }
}
