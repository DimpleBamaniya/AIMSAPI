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
        Task<Result> SaveUser(UserDto accountModel);
        Task<bool> ExecuteUpdateUserPermissions();
        Task<bool> ExecuteUpdateUseQuantity();
        Task<bool> ExecuteUpdateAvailableQuantity();
        Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity();
        Task<bool> ExecuteUpdateUserCodes();
        Task<Result> GetAllUserDetails(Pagination pagination);
    }
}
