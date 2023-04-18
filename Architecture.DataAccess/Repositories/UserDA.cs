using Architecture.Core.DataTable;
using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.DataBase.DatabaseFirst.Models;
using Architecture.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Architecture.DataAccess.Repositories
{
    public class UserDA: IUserDA
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISqlRepository<ApplicationUser> _users;
        private readonly ISqlRepository<ApplicationRole> _roles;
        private readonly IRepositoryDA<Users> _tbusers;
        private readonly ISqlRepository<IdentityUserRole<string>> _userRole;

        public UserDA(UserManager<ApplicationUser> userManager, ISqlRepository<ApplicationUser> users, ISqlRepository<ApplicationRole> roles, ISqlRepository<IdentityUserRole<string>> userRole, IRepositoryDA<Users> tbusers)
        {
            this._userRole = userRole;
            this._userManager = userManager;
            this._users = users;
            this._roles = roles;
            this._tbusers = tbusers;
        }


        public async Task<IQueryable<ApplicationUser>> GetUsers(CancellationToken cancellationToken)
        {
            return await _users.GetAsync(cancellationToken, x => x.IsActive == true);
        }

        public async Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken)
        {
            return await _roles.GetAsync(cancellationToken);
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


        public IEnumerable<Users> GetUsers()
        {
            return _tbusers.Get(includeProperties: "UserRole");
        }

        public IPagedList<Users> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return new PagedList<Users>(_tbusers.Get(includeProperties: "UserRole").ToList(), pageIndex, pageSize);
        }

        public Users GetUsersById(long userId)
        {
            if (userId == 0)
            {
                return null;
            }
            return _tbusers.GetById(userId);
        }

        public Users AddUser(Users newUser)
        {
            _tbusers.Insert(newUser);
            return newUser;
        }

        public Users UpdateUser(Users newUser)
        {
            _tbusers.Update(newUser);
            return newUser;
        }

        public bool ValidateLastChanged(string lastChanged, string userName)
        {
            return _tbusers.Table.Any(x => x.EmailId == userName && (x.UpdatedDate <= Convert.ToDateTime(lastChanged) || x.UpdatedDate == null));
        }

        public Users GetUsersByEmail(string userName)
        {
            return _tbusers.Table.FirstOrDefault(x => x.EmailId == userName);
        }
    }
}
