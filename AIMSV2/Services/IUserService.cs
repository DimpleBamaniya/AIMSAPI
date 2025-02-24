namespace Services;
public interface IUserService
{
    Task<Result> GetUserDetailByEmailIDAsync(LoginDto loginRequest);
    Task<Result> GetUserDetailByIDAsync(int id);
    Task<Result> SaveUserAsync(UserDto userModel);
    Task<bool> ExecuteUpdateUserPermissions();
    Task<bool> ExecuteUpdateUserCodes();
    Task<Result> GetAllUserDetailsAsync(Pagination pagination);
    Task<Result> DeleteUserAsync(DeleteUserDto userModel);


}
