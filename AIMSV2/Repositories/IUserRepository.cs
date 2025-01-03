using AIMSV2.Models;
using Entities;

namespace AIMSV2.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<Entities.Users>> GetAllUsers();
        Task<Users> GetUserDetailByID(int id);
        Task<Entities.Users> SaveUser(Entities.Users user);
        Task<Users> GetUserDetailByEmailID(string emailID);
        Task ExecuteUpdateUserPermissions();
        Task ExecuteUpdateUseQuantity();
        Task ExecuteUpdateAvailableQuantity();
        Task ExecuteUpdateAvailableQuantityAndUseQuantity();

    }
}
