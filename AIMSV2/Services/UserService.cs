using System.Net;
using AIMSV2.Repositories;
using AIMSV2.Utility;
using AutoMapper;
using Entities;
using AIMSV2.Models;

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

        public async Task<IEnumerable<Entities.Users>> GetAllUsers()
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
    }
}
