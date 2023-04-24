using Architecture.Dto.DataTable;
using Architecture.Dto.Role;
using Architecture.Entities.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Interface
{
    public interface IRoleBL
    {
        public Task<IQueryable<ApplicationRole>> GetAllRoleData(CancellationToken cancellationToken);

        public Task<IList<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken);

        public Task<JsonRepsonse<RoleResponseDto>> GetFilterRoleData(RoleDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken);

        //public Task<RoleRequestDto> GetRoleNameID(string Id, List<RolePermissionResponseDto> rolePermissionResponseDto, CancellationToken cancellationToken);

        public Task<RoleRequestDto> CreateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken);

        public Task<RoleRequestDto> UpdateRole(RoleRequestDto model, CancellationToken cancellationToken);

        public Task<bool> ValidateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken);

        public Task<RoleRequestDto> DeleteRole(string Id, CancellationToken cancellationToken);
    }
}
