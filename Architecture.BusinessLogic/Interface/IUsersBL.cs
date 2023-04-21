using Architecture.Dto.DataTable;
using Architecture.Dto.User;
using Architecture.Entities.Model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Architecture.BusinessLogic.Interface
{
    public interface IUsersBL
    {
        public Task<IQueryable<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken);

        public Task<IList<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken);

        public Task<IQueryable<IdentityUserRole<string>>> GetUserRoles(CancellationToken cancellationToken);

        public Task<JsonRepsonse<UserResponseDto>> GetFilterUserData(UserDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken);

        public Task<UserRequestDto> GetById(int Id, CancellationToken cancellationToken);

        public Task<UserRequestDto> CreateUser(UserRequestDto model, CancellationToken cancellationToken);

        public Task<UserRequestDto> UpdateUser(int Id, UserRequestDto model, CancellationToken cancellationToken);

        public Task<UserRequestDto> DeleteUser(int Id, CancellationToken cancellationToken);

        public Task<UserRequestDto> ReactiveUser(int Id, CancellationToken cancellationToken);

        public Task<bool> ValidateUserEmail(UserRequestDto userRequestDto, CancellationToken cancellationToken);

        public Task<bool> ValidateUserPhoneNumber(UserRequestDto userRequestDto, CancellationToken cancellationToken);

        public Task<bool> ValidateOldPassword(string currentPassword, CancellationToken cancellationToken);

        public Task<UserRequestDto> ChangePassword(ChangePasswordRequestDto model, CancellationToken cancellationToken);

        public Task<UserRequestDto> ResetPassword(ResetPasswordRequestDto model, CancellationToken cancellationToken);
    }
}
