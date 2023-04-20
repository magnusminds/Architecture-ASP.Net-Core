using Architecture.BusinessLogic.UnitOfWork;
using Architecture.Dto;
using Architecture.Dto.User;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace Architecture.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly CurrentUser _currentUser;
        private readonly IUnitOfWorkBL _unitOfWorkBL;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetUser(int userId, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.UserBL.GetById(userId, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "No Data found", result: null, statusCode: 404);
            }
            return new ApiResponse(message: "Data found", result: null, statusCode: 200);
        }


        [HttpPost]
        public async Task<ApiResponse> CreateUser([FromBody] UserRequestDto userApiRequest, CancellationToken cancellationToken)
        {
            ValidationRequest(userApiRequest);
            var data = await _unitOfWorkBL.UserBL.CreateUser(userApiRequest, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data inserted successful", result: null, statusCode: 200);
        }


        [HttpPut("{userId}")]
        public async Task<ApiResponse> UpdateUser(int userId, [FromBody] UserRequestDto userApiRequest, CancellationToken cancellationToken)
        {
            ValidationRequest(userApiRequest);
            var data = await _unitOfWorkBL.UserBL.UpdateUser(userId, userApiRequest, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data updated successful", result: null, statusCode: 200);
        }

        [HttpDelete("User/{userId}")]
        public async Task<ApiResponse> DeleteUser(int userId, CancellationToken cancellationToken)
        {
            var data = await _unitOfWorkBL.UserBL.DeleteUser(userId, cancellationToken);
            if (data == null)
            {
                return new ApiResponse(message: "Internal server error", result: null, statusCode: 500);
            }
            return new ApiResponse(message: "Data Update successful", result: null, statusCode: 200);
        }

        #region Private Methods

        private void ValidationRequest(UserRequestDto orderRequestDto)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }
        }

        #endregion Private Methods

    }
}
