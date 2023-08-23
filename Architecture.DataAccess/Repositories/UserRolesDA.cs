using Architecture.DataAccess.Interface;

namespace Architecture.DataAccess.Repositories
{
    public class UserRolesDA : IUserRolesDA
    {
        //private readonly ISqlRepository<ApplicationRole> _usersRole;
        //public UserRolesDA(ISqlRepository<ApplicationRole> usersRole)
        //{
        //    _usersRole = usersRole;

        //}

        //public IEnumerable<ApplicationRole> GetUserRoles()
        //{
        //    return _usersRole.Table.AsEnumerable();
        //}

        //public IPagedList<ApplicationRole> GetUsersPaging(int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    return new PagedList<ApplicationRole>(_usersRole.Table, pageIndex, pageSize);
        //}

        //public ApplicationRole GetUserRoleById(long userId)
        //{
        //    if (userId == 0)
        //    {
        //        return null;
        //    }
        //    return _usersRole.GetById(userId);
        //}

        //public async Task<ApplicationRole> AddUserRole(ApplicationRole model)
        //{
        //    return await _usersRole.(model);
        //}

        //public async Task<ApplicationRole> UpdateUserRole(ApplicationRole model)
        //{
        //    return await _usersRole.UpdateAsync(model);
        //}
    }
}