using AIMSV2.Entities;
using AIMSV2.Models;
using AIMSV2.Utility;
using Entities;

namespace AIMSV2.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Result> GetUserDetailByEmailID(LoginDto loginRequest);
        Task<Result> GetUserDetailByID(int id);
        Task<Result> SaveUser(UserDto userModel);
        Task<bool> ExecuteUpdateUserPermissions();
        Task<bool> ExecuteUpdateUserCodes();
        Task<Result> GetAllUserDetails(Pagination pagination);
        Task<Result> DeleteUser(DeleteUserDto userModel);

    }
}
