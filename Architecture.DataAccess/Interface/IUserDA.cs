using Architecture.Entities;
using Microsoft.AspNetCore.Identity;
using Architecture.Core.DataTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DataBase.DatabaseFirst.Models;
using Architecture.Core.DataTable;

namespace Architecture.DataAccess.Interface
{
    public interface IUserDA
    {
        public Task<IQueryable<ApplicationUser>> GetUsers(CancellationToken cancellationToken);

        public Task<IQueryable<ApplicationRole>> GetRoles(CancellationToken cancellationToken);

        public Task<string> GetUserRoleId(string Id, CancellationToken cancellationToken);

        public Task<IQueryable<IdentityUserRole<string>>> GetUserRoleData(string Id, CancellationToken cancellationToken);

        public Task<IdentityResult> CreateUser(ApplicationUser model, string password);

        public Task<IdentityResult> UpdateUser(ApplicationUser model);

        IEnumerable<Users> GetUsers();
        Users GetUsersById(long newsId);
        Users GetUsersByEmail(string userName);
        IPagedList<Users> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue);
        Users AddUser(Users newUser);
        Users UpdateUser(Users newUser);
        bool ValidateLastChanged(string lastChanged, string userName);
    }
}
