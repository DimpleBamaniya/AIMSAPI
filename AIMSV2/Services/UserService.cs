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
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Started fetching all users.");

                // Log debug information before calling the repository
                _logger.LogDebug("Calling repository method to get all users.");

                var users = await _userRepository.GetAllUsers();

                _logger.LogDebug("Fetched {UserCount} users.", users.Count());

                _logger.LogInformation("Completed fetching all users.");

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all users.");
                throw new Exception("An error occurred while fetching all users.", ex);
            }
        }


        public async Task<bool> ExecuteUpdateUserPermissions()
        {
            try
            {
                _logger.LogInformation("Started executing user permissions update.");

                // Log debug information before calling the repository
                _logger.LogDebug("Calling repository method to update user permissions.");

                await _userRepository.ExecuteUpdateUserPermissions();

                _logger.LogInformation("Completed user permissions update successfully.");

                return true;
            }
            catch (Exception ex)
            {
                // Log the error with exception details
                _logger.LogError(ex, "An error occurred while updating user permissions.");

                return false;
            }
        }

        public async Task<bool> ExecuteUpdateUserCodes()
        {
            try
            {
                _logger.LogInformation("Started executing user codes update.");

                // Log debug information before calling the repository
                _logger.LogDebug("Calling repository method to update user codes.");

                await _userRepository.ExecuteUpdateUserCodes();

                _logger.LogInformation("Completed user codes update successfully.");

                return true;
            }
            catch (Exception ex)
            {
                // Log the error with exception details
                _logger.LogError(ex, "An error occurred while updating user codes.");

                return false;
            }
        }

        public async Task<Result> GetUserDetailByEmailID(LoginDto loginModel)
        {
            try
            {
                _logger.LogInformation("Started executing GetUserDetailByEmailID.");

                // Log debug information before calling the validation
                _logger.LogDebug("Validating login fields for EmailID: {EmailID}", loginModel.EmailID);

                #region API Validations
                Result validationResult = await ValiadationFields(loginModel);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    _logger.LogDebug("Validation failed for EmailID: {EmailID}, returning validation result.", loginModel.EmailID);
                    return validationResult;
                }
                #endregion

                // Log debug information before calling the repository
                _logger.LogDebug("Fetching user details for EmailID: {EmailID}", loginModel.EmailID);

                Users users = await _userRepository.GetUserDetailByEmailID(loginModel.EmailID);
                if (users == null)
                {
                    _logger.LogDebug("User not found for EmailID: {EmailID}", loginModel.EmailID);
                    return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
                }
                else if (!String.Equals(users.EmailID, loginModel.EmailID, StringComparison.Ordinal))
                {
                    _logger.LogDebug("EmailID mismatch for EmailID: {EmailID}", loginModel.EmailID);
                    return new Result($"EmailID is incorrect.", "Accounts.EmailIsIncorrect", HttpStatusCode.BadRequest);
                }
                else if (!String.Equals(users.Password, loginModel.Password, StringComparison.Ordinal))
                {
                    _logger.LogDebug("Password mismatch for EmailID: {EmailID}", loginModel.EmailID);
                    return new Result($"Password is incorrect.", "Accounts.PasswordIsIncorrect", HttpStatusCode.BadRequest);
                }
                else
                {
                    _logger.LogDebug("User validated successfully for EmailID: {EmailID}", loginModel.EmailID);

                    LoginUserDetailsDto loginUserDetailsDto = new LoginUserDetailsDto
                    {
                        ID = users.ID,
                        Permissions = users.Permissions,
                        FirstName = users.FirstName
                    };

                    return new Result { ResultObject = loginUserDetailsDto };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details by EmailID.");

                return new Result($"{ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }
        }

        private async Task<Result> ValiadationFields(LoginDto loginModel)
        {
            try
            {
                _logger.LogInformation("Started validating the login model.");

                // Log debug information to show the incoming model details
                _logger.LogDebug("Validating login model: EmailID = {EmailID}, Password = {Password}",
                                  loginModel.EmailID, loginModel.Password);

                if (loginModel == null)
                {
                    _logger.LogDebug("Login model is null.");
                    return new Result($"Login Model is null", "Accounts.AccountModelRequired", HttpStatusCode.BadRequest);
                }
                if (loginModel.EmailID == null)
                {
                    _logger.LogDebug("EmailID is missing in the login model.");
                    return new Result($"EmailID is mandatory.", "Accounts.EmailIsMandatory", HttpStatusCode.BadRequest);
                }
                if (loginModel.Password == null)
                {
                    _logger.LogDebug("Password is missing in the login model.");
                    return new Result($"Password is mandatory.", "Accounts.PasswordIsMandatory", HttpStatusCode.BadRequest);
                }

                _logger.LogInformation("Validation completed successfully for login model.");
                return new Result();
            }
            catch (Exception ex)
            {
                // Log any errors that happen during validation
                _logger.LogError(ex, "An error occurred while validating the login model.");

                return new Result($"An error occurred: {ex.Message}", "Accounts.ValidationError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> GetUserDetailByID(int id)
        {
            try
            {
                _logger.LogInformation("Started fetching user details for UserID = {UserID}", id);

                // Log the incoming ID for debugging purposes
                _logger.LogDebug("Validating request for UserID = {UserID}", id);

                #region API Validations
                Result validationResult = await ValiadationFieldsToGetUserDetailByID(id);
                if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
                {
                    _logger.LogDebug("Validation failed for UserID = {UserID}. Error: {ErrorMessage}", id, validationResult.Message);
                    return validationResult;
                }
                _logger.LogDebug("Validation passed for UserID = {UserID}", id);
                #endregion

                _logger.LogDebug("Fetching user details for UserID = {UserID} from repository.", id);

                Users users = await _userRepository.GetUserDetailByID(id);
                if (users == null)
                {
                    _logger.LogWarning("User not found for UserID = {UserID}", id);
                    return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved user details for UserID = {UserID}", id);
                    //UserDetails loginUserDetails = _mapper.Map<Users, UserDetails>(users); // Uncomment if needed
                    return new Result { ResultObject = users };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user details for UserID = {UserID}", id);
                return new Result($"An error occurred: {ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest, ex);
            }
        }

        private async Task<Result> ValiadationFieldsToGetUserDetailByID(int id)
        {
            try
            {
                _logger.LogDebug("Validating ID for user details retrieval. ID = {UserID}", id);

                if (id == 0)
                {
                    _logger.LogWarning("Validation failed: ID is null or zero.");
                    return new Result($"ID is null", "Accounts.IDIsRequired", HttpStatusCode.BadRequest);
                }

                _logger.LogDebug("Validation passed for ID = {UserID}", id);
                return new Result();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while validating ID for user details retrieval. ID = {UserID}", id);
                return new Result($"An error occurred: {ex.Message}", "Accounts.ValidationError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> SaveUser(UserDto userModel)
        {
            try
            {
                _logger.LogDebug("Starting to save user. User ID = {UserID}", userModel.ID);

                Users? user = null;
                LoginDto? loginModel = null;

                // Handle existing user
                if (userModel.ID > 0)
                {
                    _logger.LogDebug("Fetching existing user details for User ID = {UserID}", userModel.ID);
                    user = await _userRepository.GetUserDetailByID(userModel.ID);

                    if (user == null)
                    {
                        _logger.LogWarning("User not found. User ID = {UserID}", userModel.ID);
                        return new Result("User not exists", "Users.UserNotExists", HttpStatusCode.BadRequest);
                    }

                    user.ModifiedBy = userModel.ModifiedBy;
                    _logger.LogDebug("User found. Modifying user details for User ID = {UserID}", userModel.ID);
                }
                else
                {
                    loginModel = new LoginDto
                    {
                        EmailID = userModel.EmailID,
                        Password = userModel.Password
                    };
                    Users userExist = await _userRepository.GetUserDetailByEmailID(loginModel.EmailID);
                    if (userExist != null)
                    {
                        return new Result($"User is already exist.", "Accounts.UserExist", HttpStatusCode.BadRequest);
                    }

                    // Handle new user
                    user = new Users
                    {
                        CreatedBy = userModel.CreatedBy,
                        IsDeleted = false
                    };
                    _logger.LogDebug("Creating new user. User details = {UserDetails}", userModel);
                }

                // Map user data
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.EmailID = userModel.EmailID;
                user.Password = userModel.Password;
                user.CityID = userModel?.CityID;
                user.DepartmentID = userModel?.DepartmentID;

                // Save user and update permissions/codes
                _logger.LogDebug("Saving user with ID = {UserID}", userModel.ID);
                user = await _userRepository.SaveUser(user);
                await ExecuteUpdateUserPermissions();
                await ExecuteUpdateUserCodes();

                _logger.LogDebug("User saved successfully. User ID = {UserID}", userModel.ID);
                return new Result { ResultObject = user };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving user details for User ID = {UserID}", userModel.ID);
                return new Result("An error occurred while saving user details", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> GetAllUserDetails(Pagination pagination)
        {
            try
            {
                _logger.LogDebug("Starting to fetch all user details with pagination parameters: {Pagination}", pagination);

                List<UserDetailsList> userData = (await _userRepository.GetAllUserDetails(pagination)).ToList();

                _logger.LogDebug("Successfully fetched {UserCount} user details.", userData.Count);

                return new Result { ResultObject = userData };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all user details. Pagination: {Pagination}", pagination);
                return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Result> DeleteUser(DeleteUserDto userModel)
        {
            try
            {
                _logger.LogDebug("Starting to delete user with ID: {UserID}", userModel.ID);

                Users? user = null;

                if (userModel.ID > 0)
                {
                    user = await _userRepository.GetUserDetailByID(userModel.ID);

                    if (user == null)
                    {
                        _logger.LogWarning("User with ID {UserID} not found.", userModel.ID);
                        return new Result("User not exists", "Users.UserNotExists", HttpStatusCode.BadRequest);
                    }

                    _logger.LogInformation("User with ID {UserID} found. Proceeding to delete.", userModel.ID);
                    user.DeletedBy = userModel.DeletedBy;
                }

                user = await _userRepository.DeleteUser(user);

                _logger.LogInformation("User with ID {UserID} deleted successfully.", userModel.ID);

                return new Result { ResultObject = user };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with ID: {UserID}", userModel.ID);
                return new Result("An error occurred while deleting user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
