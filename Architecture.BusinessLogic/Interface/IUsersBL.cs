using Architecture.Entities;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.Repositories
{
    public interface IUsersBL
    {
        List<UsersEntity> GetUsersEntity(int pageIndex = 0, int pageSize = int.MaxValue);
        UsersEntity CreataUser(UsersEntity user);
        UsersEntity UpdateUser(UsersEntity user);
        UsersEntity GetUsersEntityById(long userId);
        LoginViewModelResponse Validateuser(string userName, string password);
        bool CheckEmail(string userName);
        void GeneratePassword(string userId, string password, string passwordEncryptionSecurityKey);
    }
}
