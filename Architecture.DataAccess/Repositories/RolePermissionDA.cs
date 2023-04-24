﻿using Architecture.Core.Constants;
using Architecture.DataAccess.Interface;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Architecture.DataAccess.Repositories
{
    public class RolePermissionDA : IRolePermissionDA
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolePermissionDA(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IList<Claim>> GetRoleClaimsByRole(ApplicationRole applicationRole, CancellationToken cancellationToken)
        {
            return await _roleManager.GetClaimsAsync(applicationRole);
        }

        public async Task<bool> GetRoleHasPermission(string applicationRole, string permission, CancellationToken cancellationToken)
        {
            var roleByName = await _roleManager.FindByIdAsync(applicationRole);
            var roleClaims = await GetRoleClaimsByRole(roleByName, cancellationToken);
            return roleClaims.Any(a => a.Value == permission);
        }

        public async Task<IdentityResult> CreateRolePermission(ApplicationRole applicationRole, string claimValue, CancellationToken cancellationToken)
        {
            return await _roleManager.AddClaimAsync(applicationRole, new Claim(CustomClaimTypes.Permission, claimValue));
        }

        public async Task<IdentityResult> DeleteRolePermission(ApplicationRole applicationRole, string claimValue, CancellationToken cancellationToken)
        {
            return await _roleManager.RemoveClaimAsync(applicationRole, new Claim(CustomClaimTypes.Permission, claimValue));
        }

        public async Task<IdentityResult> CreateRolePermissionClaims(ApplicationRole applicationRole, string permission, CancellationToken cancellationToken)
        {
            return await _roleManager.AddClaimAsync(applicationRole, new Claim(CustomClaimTypes.Permission, permission));
        }
    }
}
