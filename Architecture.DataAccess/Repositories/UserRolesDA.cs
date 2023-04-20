using Architecture.DataAccess.Interface;
namespace Architecture.DataAccess.Repositories
{
    public class UserRolesDA : IUserRolesDA
    {
        //private readonly ISqlRepository<UserRole> _usersRole;
        //public UserRolesDA(ISqlRepository<UserRole> usersRole)
        //{
        //    _usersRole = usersRole;

        //}

        //public IEnumerable<UserRole> GetUserRoles()
        //{
        //    return _usersRole.Table.AsEnumerable();
        //}

        //public IPagedList<UserRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    return new PagedList<UserRole>(_usersRole.Table, pageIndex, pageSize);
        //}

        //public UserRole GetUserRoleById(long userId)
        //{
        //    if (userId == 0)
        //    {
        //        return null;
        //    }
        //    return _usersRole.GetById(userId);
        //}

        //public async Task<IdentityResult> CreateRole(ApplicationRole model)
        //{
        //    return await _usersRole.CreateAsync(model);
        //}

        //public async Task<IdentityResult> UpdateRole(ApplicationRole model)
        //{
        //    return await _usersRole.UpdateAsync(model);
        //}
    }
}
