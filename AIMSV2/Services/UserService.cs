using System.Net;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;
using AIMSV2.Models;
using AIMSV2.Entities;

namespace AIMSV2.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<bool> ExecuteUpdateUserPermissions()
        {
            try
            {
                await _userRepository.ExecuteUpdateUserPermissions();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateUseQuantity()
        {
            try
            {
                await _userRepository.ExecuteUpdateUseQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantity()
        {
            try
            {
                await _userRepository.ExecuteUpdateAvailableQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExecuteUpdateAvailableQuantityAndUseQuantity()
        {
            try
            {
                await _userRepository.ExecuteUpdateAvailableQuantityAndUseQuantity();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<Result> GetUserDetailByEmailID(LoginDto loginModel)
        {
            try
            {
                #region API Validations
                Result validationResult = await ValiadationFields(loginModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return validationResult;
                }
                #endregion

                Users users = await _userRepository.GetUserDetailByEmailID(loginModel.EmailID);
                if (users == null)
                {
                    return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
                }
                else if (!String.Equals(users.EmailID, loginModel.EmailID, StringComparison.Ordinal))
                {
                    return new Result($"EmailID is incorrect.", "Accounts.EmailIsIncorrect", HttpStatusCode.BadRequest);
                }
                else if (!String.Equals(users.Password, loginModel.Password, StringComparison.Ordinal))
                {
                    return new Result($"Password is incorrect.", "Accounts.PasswordIsIncorrect", HttpStatusCode.BadRequest);
                }
                else
                {
                    LoginUserDetailsDto loginUserDetailsDto = new LoginUserDetailsDto();
                    loginUserDetailsDto.ID = users.ID;
                    loginUserDetailsDto.Permissions = users.Permissions;
                    return new Result { ResultObject = loginUserDetailsDto };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }

        }

        private async Task<Result> ValiadationFields(LoginDto loginModel)
        {
            if (loginModel == null)
            {
                return new Result($"Login Model is null", "Accounts.AccountModelRequired", HttpStatusCode.BadRequest);
            }
            if (loginModel.EmailID == null)
            {
                return new Result($"EmailID is mandatory.", "Accounts.EmailIsMandatory", HttpStatusCode.BadRequest);
            }
            if (loginModel.Password == null)
            {
                return new Result($"Password is mandatory.", "Accounts.PasswordIsMandatory", HttpStatusCode.BadRequest);
            }

            return new Result();
        }
        public async Task<Result> GetUserDetailByID(int id)
        {
            try
            {
                #region API Validations
                Result validationResult = await ValiadationFieldsToGetUserDetailByID(id);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    return validationResult;
                }
                #endregion

                Users users = await _userRepository.GetUserDetailByID(id);
                if (users == null)
                {
                    return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
                }
                else
                {
                    UserDetails loginUserDetails = _mapper.Map<Users, UserDetails>(users);
                    return new Result { ResultObject = loginUserDetails };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }

        }

        private async Task<Result> ValiadationFieldsToGetUserDetailByID(int id)
        {
            if (id == null)
            {
                return new Result($"ID is null", "Accounts.IDIsRequired", HttpStatusCode.BadRequest);
            }
            return new Result();
        }

        public async Task<Result> SaveUser(UserDto userModel)
        {
            try
            {

                Users? user = null;

                if (userModel.ID > 0)
                {
                    user = await _userRepository.GetUserDetailByID(userModel.ID);

                    if (user == null)
                    {
                        return new Result("User not exists", "Users.UserNotExists", HttpStatusCode.BadRequest);
                    }

                    //user = _mapper.Map<UserDetails, Entities.Users>(user);

                    user.ModifiedBy = userModel.ID;
                }
                else
                {
                    user = new Users
                    {
                        CreatedBy = userModel.ID,
                        IsDeleted = false
                    };
                }
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.EmailID = userModel.EmailID;
                user.Password = userModel.Password;
                user.CityID = userModel.CityID;
                user.DepartmentID = userModel.DepartmentID;

                user = await _userRepository.SaveUser(user);
                // letter change
                user.CreatedBy = user.ID;
                return new Result { ResultObject = user };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while save user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<Result> GetAllUserDetails(Pagination pagination)
        {
            try
            {
                List<UserDetailsList> userData = (await _userRepository.GetAllUserDetails(pagination)).ToList();
                return new Result { ResultObject = userData };
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
