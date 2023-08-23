using Architecture.BusinessLogic.Interface;
using Architecture.Core.UserDefinedException;
using Architecture.DataAccess.UnitOfWork;
using Architecture.Dto;
using Architecture.Dto.DataTable;
using Architecture.Dto.Paging;
using Architecture.Dto.Role;
using Architecture.Entities.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Repositories
{
    public class RoleBL : IRoleBL
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUnitOfWorkDA _unitOfWorkDA;
        private readonly CurrentUser _currentUser;
        private readonly IMapper _mapper;

        public RoleBL(IUnitOfWorkDA unitOfWorkDA, CurrentUser currentUser, IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _unitOfWorkDA = unitOfWorkDA;
            _currentUser = currentUser;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<IQueryable<ApplicationRole>> GetAllRoleData(CancellationToken cancellationToken)
        {
            var getAllRole = await _unitOfWorkDA.RoleDA.GetRoles(cancellationToken);
            if (getAllRole == null)
            {
                throw new SystemInternalException("Role data not found");
            }
            return getAllRole;
        }

        public async Task<IList<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken)
        {
            var getUser = await GetAllRoleData(cancellationToken);
            var rolesByTenant = getUser.Where(x => x.TenantId == _currentUser.TenantId).ToList();
            foreach (var item in rolesByTenant)
            {
                item.Name = item.Name.Replace("_" + Convert.ToString(_currentUser.TenantId), "");
                item.NormalizedName = item.NormalizedName.Replace("_" + Convert.ToString(_currentUser.TenantId), "");
            }
            return rolesByTenant;
        }

        public async Task<JsonRepsonse<RoleResponseDto>> GetFilterRoleData(RoleDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken)
        {
            var roleAllData = await GetAllRoleData(cancellationToken);
            var role = from x in roleAllData
                       where x.TenantId == _currentUser.TenantId
                       select new RoleResponseDto
                       {
                           Id = x.Id,
                           Name = x.Name.Replace("_" + Convert.ToString(_currentUser.TenantId), ""),
                           NormalizedName = x.NormalizedName.Replace("_" + Convert.ToString(_currentUser.TenantId), ""),
                           TenantId = x.TenantId,
                           IsDeleted = x.IsDeleted
                       };
            var roleFilterData = FilterRoleData(dataTableFilterDto, role);
            var roleData = new PagedList<RoleResponseDto>(roleFilterData, dataTableFilterDto);
            return new JsonRepsonse<RoleResponseDto>(dataTableFilterDto.Draw, roleData.TotalCount, roleData.TotalCount, roleData);
        }

        //public async Task<RoleRequestDto> GetRoleNameID(string Id, List<RolePermissionResponseDto> rolePermissionResponseDto, CancellationToken cancellationToken)
        //{
        //    RoleRequestDto mRoleRequestDto = new RoleRequestDto();
        //    var roleData = await GetAllRoles(cancellationToken);
        //    var roleName = roleData.Where(x => x.Id == Id).Select(n => n.Name).FirstOrDefault();
        //    mRoleRequestDto.Id = Id;
        //    mRoleRequestDto.Name = roleName.Replace("_" + Convert.ToString(_currentUser.TenantId), "");
        //    mRoleRequestDto.ModulePermissionList.AddRange(rolePermissionResponseDto);
        //    return mRoleRequestDto;
        //}

        public async Task<RoleRequestDto> CreateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken)
        {
            ApplicationRole addRoleData = new ApplicationRole();
            addRoleData.Name = roleRequestDto.Name + "_" + Convert.ToString(_currentUser.TenantId);
            addRoleData.TenantId = _currentUser.TenantId;
            var insertedRole = await _unitOfWorkDA.RoleDA.CreateRole(addRoleData);
            return _mapper.Map<RoleRequestDto>(insertedRole);
        }

        public async Task<RoleRequestDto> UpdateRole(RoleRequestDto model, CancellationToken cancellationToken)
        {
            var data = await _roleManager.FindByIdAsync(model.Id);
            data.Name = model.Name;
            data.Name = data.Name.Replace("_" + Convert.ToString(_currentUser.TenantId), "") + "_" + Convert.ToString(_currentUser.TenantId);
            var updateRole = await _unitOfWorkDA.RoleDA.UpdateRole(data);
            return _mapper.Map<RoleRequestDto>(updateRole);
        }

        // Role Name Validation
        public async Task<bool> ValidateRole(RoleRequestDto roleRequestDto, CancellationToken cancellationToken)
        {
            var getAllRole = await _unitOfWorkDA.RoleDA.GetRoles(cancellationToken);

            if (roleRequestDto.Id != null)
            {
                var getRoleById = getAllRole.First(p => p.Id == roleRequestDto.Id);
                if (getRoleById.Name.Trim() == roleRequestDto.Name.Trim())
                {
                    return true;
                }
                else
                {
                    return !getAllRole.Any(p => p.Name.Trim() == roleRequestDto.Name.Trim() + "_" + Convert.ToString(_currentUser.TenantId) && p.Id != roleRequestDto.Id);
                }
            }
            else
            {
                return !getAllRole.Any(p => p.Name.Trim() == roleRequestDto.Name.Trim() + "_" + Convert.ToString(_currentUser.TenantId));
            }
        }

        public async Task<RoleRequestDto> DeleteRole(string Id, CancellationToken cancellationToken)
        {
            // InActive AspNetRoles
            var getRoles = await _unitOfWorkDA.RoleDA.GetRoles(cancellationToken);
            var RoleById = getRoles.Where(x => x.Id == Id).FirstOrDefault();
            RoleById.IsDeleted = true;
            await _unitOfWorkDA.RoleDA.UpdateRole(_mapper.Map<ApplicationRole>(RoleById));
            return _mapper.Map<RoleRequestDto>(RoleById);
        }

        #region Private Method

        private static IQueryable<RoleResponseDto> FilterRoleData(RoleDataTableFilterDto roleDataTableFilterDto, IQueryable<RoleResponseDto> roleResponseDto)
        {
            if (roleDataTableFilterDto != null)
            {
                if (!string.IsNullOrEmpty(roleDataTableFilterDto.RoleName))
                {
                    roleResponseDto = roleResponseDto.Where(p => p.Name.Contains(roleDataTableFilterDto.RoleName));
                }
            }
            return roleResponseDto;
        }

        #endregion Private Method
    }
}