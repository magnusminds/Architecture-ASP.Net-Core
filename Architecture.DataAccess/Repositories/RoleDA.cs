using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.Dto;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;

namespace Architecture.DataAccess.Repositories
{
    public class RoleDA : IRoleDA
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ISqlRepository<ApplicationRole> _roles;
        private readonly CurrentUser _currentUser;

        public RoleDA(RoleManager<ApplicationRole> roleManager, ISqlRepository<ApplicationRole> roles, CurrentUser currentUser)
        {
            this._roleManager = roleManager;
            this._roles = roles;
            this._currentUser = currentUser;
        }

        public async Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken)
        {
            return await _roles.GetAsync(cancellationToken, x => x.TenantId == _currentUser.TenantId && x.IsDeleted == false);
        }

        public async Task<IQueryable<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken)
        {
            return await _roles.GetAsync(cancellationToken, x => x.IsDeleted == false);
        }

        public async Task<IdentityResult> CreateRole(ApplicationRole model)
        {
            return await _roleManager.CreateAsync(model);
        }

        public async Task<IdentityResult> UpdateRole(ApplicationRole model)
        {
            return await _roleManager.UpdateAsync(model);
        }
    }
}