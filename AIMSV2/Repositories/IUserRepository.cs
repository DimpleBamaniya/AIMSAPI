using AIMSV2.Entities;
using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserDetailByID(int id);
        Task<Users> SaveUser(Users user);
        Task<Users> GetUserDetailByEmailID(string emailID);
        Task ExecuteUpdateUserPermissions();
        Task ExecuteUpdateUserCodes();
        Task<List<UserDetailsList>> GetAllUserDetails(Pagination pagination);
    }
}
