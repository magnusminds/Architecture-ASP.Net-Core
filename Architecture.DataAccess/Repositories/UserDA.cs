﻿using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;


namespace Architecture.DataAccess.Repositories
{
    public class UserDA: IUserDA
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISqlRepository<ApplicationUser> _users;
        private readonly ISqlRepository<ApplicationRole> _roles;
       
        private readonly ISqlRepository<IdentityUserRole<string>> _userRole;

        public UserDA(UserManager<ApplicationUser> userManager, ISqlRepository<ApplicationUser> users, ISqlRepository<ApplicationRole> roles, ISqlRepository<IdentityUserRole<string>> userRole)
        {
            this._userRole = userRole;
            this._userManager = userManager;
            this._users = users;
            this._roles = roles;
           
        }

        public async Task<IQueryable<ApplicationUser>> GetUsers(CancellationToken cancellationToken)
        {
            return await _users.GetAsync(cancellationToken);
        }

        public async Task<IQueryable<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken)
        {
            var allUsers = await _users.GetAsync(cancellationToken);
            allUsers.ToList().ForEach(x => x.LastName = x.IsActive == false ? x.LastName + " (inactive)" : x.LastName);
            return allUsers;
        }

        public async Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken)
        {
            return await _roles.GetAsync(cancellationToken);
        }

        public async Task<IQueryable<IdentityUserRole<string>>> GetAspNetUserRoles(CancellationToken cancellationToken)
        {
            return await _userRole.GetAsync(cancellationToken);
        }

        public async Task<string> GetUserRoleId(string Id, CancellationToken cancellationToken)
        {
            var data = await _userRole.GetAsync(cancellationToken);
            var roleId = data.Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            return roleId;
        }

        public async Task<IQueryable<IdentityUserRole<string>>> GetUserRoleData(string Id, CancellationToken cancellationToken)
        {
            var data = await _userRole.GetAsync(cancellationToken);
            var roleData = data.Where(x => x.UserId == Id);
            return roleData;
        }

        public async Task<IdentityResult> CreateUser(ApplicationUser model, string password)
        {
            return await _userManager.CreateAsync(model, password);
        }

        public async Task<IdentityResult> UpdateUser(ApplicationUser model)
        {
            return await _userManager.UpdateAsync(model);
        }
    }
}
