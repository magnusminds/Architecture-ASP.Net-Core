using Architecture.BusinessLogic.Interface;
using Architecture.Core.Constants;
using Architecture.Core.UserDefinedException;
using Architecture.DataAccess.UnitOfWork;
using Architecture.Dto;
using Architecture.Dto.DataTable;
using Architecture.Dto.Paging;
using Architecture.Dto.User;
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
    public class UsersBL : IUsersBL
    {
        private readonly IUnitOfWorkDA _unitOfWorkDA;
        private readonly CurrentUser _currentUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public UsersBL(IUnitOfWorkDA unitOfWorkDA, CurrentUser currentUser, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _unitOfWorkDA = unitOfWorkDA;
            _currentUser = currentUser;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IQueryable<ApplicationUser>> GetAllUsers(CancellationToken cancellationToken)
        {
            var getUser = await GetUsersData(cancellationToken);
            return getUser;
        }

        public async Task<IList<ApplicationRole>> GetAllRoles(CancellationToken cancellationToken)
        {
            var getRole = await _unitOfWorkDA.UserDA.GetRoles(cancellationToken);
            var rolesByTenant = getRole.Where(x => x.TenantId == _currentUser.TenantId).ToList();
            return rolesByTenant;
        }

        public async Task<IQueryable<IdentityUserRole<string>>> GetUserRoles(CancellationToken cancellationToken)
        {
            var getUserRole = await _unitOfWorkDA.UserDA.GetAspNetUserRoles(cancellationToken);
            return getUserRole;
        }

        public async Task<JsonRepsonse<UserResponseDto>> GetFilterUserData(UserDataTableFilterDto dataTableFilterDto, CancellationToken cancellationToken)
        {
            var allUser = await _unitOfWorkDA.UserDA.GetUsers(cancellationToken);
            var aspNetUserRoles = await _unitOfWorkDA.UserDA.GetAspNetUserRoles(cancellationToken);
            var aspnetRole = await _unitOfWorkDA.RoleDA.GetRoles(cancellationToken);
            // var manufacturingWorkflowMapping = await _unitOfWorkDA.ManufacturingWorkflowMappingDA.GetAll(cancellationToken);
            var userResponseDto = (from x in allUser
                                   join y in aspNetUserRoles on x.Id equals y.UserId into AspNetUserRolesMapingDetails
                                   from AspNewUserRolesMat in AspNetUserRolesMapingDetails.DefaultIfEmpty()
                                   join z in aspnetRole on AspNewUserRolesMat.RoleId equals z.Id into AspNetUserRolesDetails
                                   from t in AspNetUserRolesDetails.DefaultIfEmpty()
                                       //join a in await _unitOfWorkDA.UserTenantMappingDA.GetAll(cancellationToken)
                                       //  on new { x.UserId } equals new { a.UserId }
                                   where x.UserId == _currentUser.TenantId
                                   orderby x.UserId ascending
                                   select new UserResponseDto
                                   {
                                       FirstName = x.FirstName,
                                       LastName = x.LastName,
                                       UserName = x.UserName,
                                       Email = x.Email,
                                       UserRole = t.Name.Replace("_" + Convert.ToString(_currentUser.TenantId), ""),
                                       PhoneNumber = x.PhoneNumber,
                                       UserId = x.UserId,
                                       IsActive = x.IsActive,
                                   }).Where(x => !(x.UserRole == AspNetRolesEnum.Contractor.ToString() || x.UserRole == AspNetRolesEnum.StoreManager.ToString() || x.UserRole == AspNetRolesEnum.Supervisor.ToString()));

            var userFilteredData = FilterUserData(dataTableFilterDto, userResponseDto);
            var userData = new PagedList<UserResponseDto>(userFilteredData, dataTableFilterDto);
            return new JsonRepsonse<UserResponseDto>(dataTableFilterDto.Draw, userData.TotalCount, userData.TotalCount, userData);
        }

        public async Task<UserRequestDto> GetById(int Id, CancellationToken cancellationToken)
        {
            // Get ApplicationUser by Id
            var userAllData = await GetAllUsersData(cancellationToken);
            var applicationUser = (from x in userAllData
                                       //join y in await _unitOfWorkDA.UserTenantMappingDA.GetAll(cancellationToken)
                                       //on new { x.Id } equals new { y.UserId }
                                   where x.UserId == Id
                                   select new UserRequestDto
                                   {
                                       Id = x.Id,
                                       FirstName = x.FirstName,
                                       LastName = x.LastName,
                                       UserName = x.UserName,
                                       Email = x.Email,
                                       PhoneNumber = x.PhoneNumber,
                                       MobileDeviceId = x.MobileDeviceId,
                                       UserId = x.UserId,
                                       TenantId = x.UserId,
                                       UserTenantMappingId = 0
                                   }).FirstOrDefault();

            // Get User Role by roleId
            var roleId = _unitOfWorkDA.UserDA.GetUserRoleId(applicationUser.Id, cancellationToken);
            var getAllRole = await _unitOfWorkDA.RoleDA.GetRoles(cancellationToken);
            var roleDataById = getAllRole.FirstOrDefault(x => x.Id == roleId.Result);
            if (roleDataById != null)
                applicationUser.AspNetRole = roleDataById.NormalizedName;
            return applicationUser;
        }

        public async Task<UserRequestDto> CreateUser(UserRequestDto model, CancellationToken cancellationToken)
        {
            // Add User into AspNetUser
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            applicationUser.UserName = model.Email;
            applicationUser.Email = model.Email;
            applicationUser.PhoneNumber = model.PhoneNumber;
            applicationUser.IsActive = true;
            applicationUser.EmailConfirmed = true;
            applicationUser.MobileDeviceId = model.MobileDeviceId;
            await _unitOfWorkDA.UserDA.CreateUser(applicationUser, model.Password);

            // Add UserId and TenantId into UserTenantMapping
            //UserTenantMappingRequestDto userTenantData = new UserTenantMappingRequestDto();
            //userTenantData.TenantId = _currentUser.TenantId;
            //userTenantData.UserId = applicationUser.UserId;
            //await _userTenantMappingBL.CreateUserTenant(userTenantData, cancellationToken);

            // Add into AspNetRole
            await _userManager.AddToRoleAsync(applicationUser, model.AspNetRole + "_" + Convert.ToString(_currentUser.TenantId));

            return _mapper.Map<UserRequestDto>(applicationUser);
        }

        public async Task<UserRequestDto> UpdateUser(int Id, UserRequestDto model, CancellationToken cancellationToken)
        {
            // Update AspNetUser
            var userAllData = await GetAllUsersData(cancellationToken);
            var oldApplicationUserData = userAllData.Where(p => p.UserId == Id).FirstOrDefault();
            oldApplicationUserData.FirstName = model.FirstName;
            oldApplicationUserData.LastName = model.LastName;
            oldApplicationUserData.PhoneNumber = model.PhoneNumber;
            oldApplicationUserData.UserName = model.Email;
            oldApplicationUserData.Email = model.Email;
            oldApplicationUserData.MobileDeviceId = model.MobileDeviceId;
            var updateUser = await _unitOfWorkDA.UserDA.UpdateUser(_mapper.Map<ApplicationUser>(oldApplicationUserData));

            // Get selected role details from AspNetUserRoles and AspNetRoles
            var rolesData = _unitOfWorkDA.UserDA.GetUserRoleData(Convert.ToString(oldApplicationUserData.Id), cancellationToken).Result.FirstOrDefault();
            if (rolesData != null)
            {
                var oldRoleNameData = await _roleManager.FindByIdAsync(rolesData.RoleId);
                //Remove old UserRole
                await _userManager.RemoveFromRoleAsync(oldApplicationUserData, oldRoleNameData.Name);
            }

            //Add Updated UserRole
            await _userManager.AddToRoleAsync(oldApplicationUserData, model.AspNetRole + "_" + Convert.ToString(_currentUser.TenantId));

            return _mapper.Map<UserRequestDto>(oldApplicationUserData);
        }

        public async Task<UserRequestDto> DeleteUser(int Id, CancellationToken cancellationToken)
        {
            // InActive AspNetUser
            var userAllData = await GetAllUsersData(cancellationToken);
            var userById = userAllData.Where(x => x.UserId == Id).FirstOrDefault();
            userById.IsActive = false;
            userById.EmailConfirmed = false;
            userById.IsDeleted = true;
            await _unitOfWorkDA.UserDA.UpdateUser(_mapper.Map<ApplicationUser>(userById));
            return _mapper.Map<UserRequestDto>(userById);
        }

        public async Task<UserRequestDto> ReactiveUser(int Id, CancellationToken cancellationToken)
        {
            // Active AspNetUser
            var userAllData = await GetAllUsersData(cancellationToken);
            var userById = userAllData.Where(x => x.UserId == Id).FirstOrDefault();
            if (userById != null)
            {
                userById.IsActive = true;
                userById.EmailConfirmed = false;
                userById.IsDeleted = false;
                await _unitOfWorkDA.UserDA.UpdateUser(_mapper.Map<ApplicationUser>(userById));
            }
            return _mapper.Map<UserRequestDto>(userById);
        }

        public async Task<bool> ValidateUserPhoneNumber(UserRequestDto userRequestDto, CancellationToken cancellationToken)
        {
            var getAllUserData = await GetAllAnonymousUsersData(cancellationToken);
            if (userRequestDto.Id != null)
            {
                var getUserById = getAllUserData.First(p => p.UserId == Convert.ToInt16(userRequestDto.Id));
                if (getUserById.PhoneNumber.Trim() == userRequestDto.PhoneNumber.Trim())
                    return true;
                else
                    return !getAllUserData.Any(p => p.PhoneNumber.Trim() == userRequestDto.PhoneNumber);
            }
            else
                return !getAllUserData.Any(p => p.PhoneNumber.Trim() == userRequestDto.PhoneNumber);
        }

        public async Task<bool> ValidateUserEmail(UserRequestDto userRequestDto, CancellationToken cancellationToken)
        {
            var getAllUserData = await GetAllAnonymousUsersData(cancellationToken);
            if (userRequestDto.Id != null)
            {
                var getUserById = getAllUserData.First(p => p.UserId == Convert.ToInt16(userRequestDto.Id));
                if (getUserById.Email.Trim() == userRequestDto.Email.Trim())
                    return true;
                else
                    return !getAllUserData.Any(p => p.Email.Trim() == userRequestDto.Email);
            }
            else
                return !getAllUserData.Any(p => p.Email.Trim() == userRequestDto.Email);
        }

        public async Task<bool> ValidateOldPassword(string OldPassword, CancellationToken cancellationToken)
        {
            var userAllData = await GetAllUsersData(cancellationToken);
            var oldApplicationUserData = userAllData.Where(p => p.Id == _currentUser.UserId).FirstOrDefault();
            var isPasswordValidate = await _userManager.CheckPasswordAsync(_mapper.Map<ApplicationUser>(oldApplicationUserData), OldPassword);
            return isPasswordValidate;
        }

        public async Task<UserRequestDto> ChangePassword(ChangePasswordRequestDto model, CancellationToken cancellationToken)
        {
            // Update AspNetUser
            var userAllData = await GetAllUsersData(cancellationToken);
            var oldApplicationUserData = userAllData.Where(p => p.Id == _currentUser.UserId).FirstOrDefault();
            var updateUser = await _userManager.ChangePasswordAsync(_mapper.Map<ApplicationUser>(oldApplicationUserData), model.OldPassword, model.ConfirmPassword);
            return _mapper.Map<UserRequestDto>(oldApplicationUserData);
        }

        public async Task<UserRequestDto> ResetPassword(ResetPasswordRequestDto model, CancellationToken cancellationToken)
        {
            var userAllData = await GetAllUsersData(cancellationToken);
            var oldApplicationUserData = userAllData.Where(p => p.UserId == model.UserId).FirstOrDefault();
            var applicationUser = _mapper.Map<ApplicationUser>(oldApplicationUserData);
            await _userManager.RemovePasswordAsync(applicationUser);
            await _userManager.AddPasswordAsync(applicationUser, model.ConfirmPassword);
            return _mapper.Map<UserRequestDto>(oldApplicationUserData);
        }

        #region Private Methods

        private async Task<IQueryable<ApplicationUser>> GetAllUsersData(CancellationToken cancellationToken)
        {
            var getAllUser = await _unitOfWorkDA.UserDA.GetUsers(cancellationToken);
            if (getAllUser == null)
            {
                throw new SystemInternalException("Users data not found");
            }
            return getAllUser;
        }

        private async Task<IQueryable<ApplicationUser>> GetUsersData(CancellationToken cancellationToken)
        {
            var getAllUser = await _unitOfWorkDA.UserDA.GetAllUsers(cancellationToken);
            if (getAllUser == null)
            {
                throw new SystemInternalException("Users data not found");
            }
            return getAllUser;
        }

        private async Task<IQueryable<ApplicationUser>> GetAllAnonymousUsersData(CancellationToken cancellationToken)
        {
            var getAllUser = await _unitOfWorkDA.UserDA.GetAllUsers(cancellationToken);
            if (getAllUser == null)
            {
                throw new SystemInternalException("Users data not found");
            }
            return getAllUser;
        }

        private static IQueryable<UserResponseDto> FilterUserData(UserDataTableFilterDto userDataTableFilterDto, IQueryable<UserResponseDto> userResponseDto)
        {
            if (userDataTableFilterDto != null)
            {
                if (!string.IsNullOrEmpty(userDataTableFilterDto.Name))
                {
                    userResponseDto = userResponseDto.Where(p => (p.FirstName.ToLower() + " " + p.LastName.ToLower()).Contains(userDataTableFilterDto.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(userDataTableFilterDto.Email))
                {
                    userResponseDto = userResponseDto.Where(p => p.Email.Contains(userDataTableFilterDto.Email));
                }
                if (!string.IsNullOrEmpty(userDataTableFilterDto.PhoneNumber))
                {
                    userResponseDto = userResponseDto.Where(p => p.PhoneNumber.Contains(userDataTableFilterDto.PhoneNumber));
                }
                if (userDataTableFilterDto.RoleName != null && userDataTableFilterDto.RoleName != "-1")
                {
                    userResponseDto = userResponseDto.Where(p => p.UserRole == userDataTableFilterDto.RoleName);
                }
                if (userDataTableFilterDto.IsActive != null && userDataTableFilterDto.IsActive != "-1")
                {
                    userResponseDto = userResponseDto.Where(p => p.IsActive == Convert.ToBoolean(userDataTableFilterDto.IsActive));
                }
            }
            return userResponseDto;
        }

        #endregion Private Methods
    }
}