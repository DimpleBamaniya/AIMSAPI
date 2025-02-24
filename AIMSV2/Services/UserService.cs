using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using Common;
using Entities;
using Microsoft.IdentityModel.Tokens;
using Models;

namespace Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private readonly IConfiguration _config;
    private readonly ICommonService _commonService;
    public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger, IConfiguration config, ICommonService commonService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _config = config;
        _commonService = commonService;
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

    public async Task<Result> GetUserDetailByEmailIDAsync(LoginDto loginModel)
    {
        try
        {
            _logger.LogInformation("Started executing GetUserDetailByEmailIDAsync.");

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

            Users users = await _userRepository.GetUserDetailByEmailIDAsync(loginModel.EmailID);
            if (users == null)
            {
                _logger.LogDebug("User not found for EmailID: {EmailID}", loginModel.EmailID);
                return new Result($"User does not exist.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }
            else if (!String.Equals(users.EmailID, loginModel.EmailID, StringComparison.Ordinal))
            {
                _logger.LogDebug("EmailID mismatch for EmailID: {EmailID}", loginModel.EmailID);
                return new Result($"User does not exist.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }
            else
            {
                if (PasswordHelper.VerifyPassword(loginModel.Password, users.Password))
                {
                    _logger.LogDebug("User validated successfully for EmailID: {EmailID}", loginModel.EmailID);

                    // Generate the token
                    var token = GenerateToken(users);
                    // Return the token wrapped in a Result object
                    return new Result
                    {
                        Status = StatusType.Success,
                        HttpStatusCode = HttpStatusCode.OK,
                        ResultObject = new
                        {
                            Token = token,
                            LoginUserDetailsDto = new
                            {
                                users.ID,
                                users.Permissions,
                                users.FirstName
                            }
                        },
                        Message = "Get User Detail by EmailID retrieved successfully."
                    };

                }
                else
                {
                    return new Result($"Password is incorrect.", "Accounts.PasswordisIncorrect", HttpStatusCode.BadRequest);
                }
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

    public async Task<Result> GetUserDetailByIDAsync(int id)
    {
        try
        {
            _logger.LogInformation("Started fetching user details for UserID = {UserID}", id);

            // Log the incoming ID for debugging purposes
            _logger.LogDebug("Validating request for UserID = {UserID}", id);

            #region API Validations
            Result validationResult = ValiadationFieldsToGetUserDetailByID(id);
            if (validationResult.HasError && (HttpStatusCode)validationResult.StatusCode == HttpStatusCode.BadRequest)
            {
                _logger.LogDebug("Validation failed for UserID = {UserID}. Error: {ErrorMessage}", id, validationResult.Message);
                return validationResult;
            }
            _logger.LogDebug("Validation passed for UserID = {UserID}", id);
            #endregion

            _logger.LogDebug("Fetching user details for UserID = {UserID} from repository.", id);

            Users users = await _userRepository.GetUserDetailByIDAsync(id);
            if (users == null)
            {
                _logger.LogWarning("User not found for UserID = {UserID}", id);
                return new Result($"User is not Exists.", "Accounts.AccountNotExists", HttpStatusCode.BadRequest);
            }
            else
            {
                _logger.LogInformation("Successfully retrieved user details for UserID = {UserID}", id);
                return new Result
                {
                    Status = StatusType.Success,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "Get User Detail By ID retrieved successfully.",
                    ResultObject = users
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching user details for UserID = {UserID}", id);
            return new Result($"An error occurred: {ex.Message}", "Accounts.AccountNotExists", HttpStatusCode.BadRequest, ex);
        }
    }

    private Result ValiadationFieldsToGetUserDetailByID(int id)
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

    public async Task<Result> SaveUserAsync(UserDto userModel)
    {
        try
        {
            _logger.LogDebug("Starting to save user. User ID = {UserID}", userModel.ID);

            Users? user = null;
            LoginDto? loginModel = null;
            var userId = _commonService.GetLoggedUserId();
            if (IsUserLoggedInUser() && userModel.ID > 0 && userId == userModel.ID)
            {
                _logger.LogWarning("You are not allowed to edit your own data. User ID = {UserID}", userModel.ID);
                return new Result("You are not allowed to edit your own data", "Users.UserCanNotDeleteOwnData", HttpStatusCode.BadRequest);
            }
            else
            {
                // Handle existing user
                if (userModel.ID > 0)
                {
                    _logger.LogDebug("Fetching existing user details for User ID = {UserID}", userModel.ID);
                    user = await _userRepository.GetUserDetailByIDAsync(userModel.ID);

                    if (user == null)
                    {
                        _logger.LogWarning("User not found. User ID = {UserID}", userModel.ID);
                        return new Result("User not exists", "Users.UserNotExists", HttpStatusCode.BadRequest);
                    }

                    user.ModifiedBy = userId;
                    _logger.LogDebug("User found. Modifying user details for User ID = {UserID}", userModel.ID);
                }
                else
                {
                    loginModel = new LoginDto
                    {
                        EmailID = userModel.EmailID,
                        Password = userModel.Password
                    };

                    Users userExist = await _userRepository.GetUserDetailByEmailIDAsync(loginModel.EmailID);
                    if (userExist != null)
                    {
                        return new Result($"User is already exist.", "Accounts.UserExist", HttpStatusCode.BadRequest);
                    }

                    // Handle new user
                    user = new Users
                    {
                        CreatedBy = userId,
                        EmailID = userModel.EmailID,
                        Password = PasswordHelper.HashPassword(userModel.Password),
                        IsDeleted = false
                    };
                    _logger.LogDebug("Creating new user. User details = {UserDetails}", userModel);
                }

                // Map user data
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.CityID = userModel?.CityID;
                user.DepartmentID = userModel?.DepartmentID;

                // Save user and update permissions/codes
                _logger.LogDebug("Saving user with ID = {UserID}", userModel.ID);
                user = await _userRepository.SaveUserAsync(user);
                await ExecuteUpdateUserPermissions();
                await ExecuteUpdateUserCodes();

                _logger.LogDebug("User saved successfully. User ID = {UserID}", userModel.ID);
                return new Result
                {
                    Status = StatusType.Success,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "User saved successfully",
                    ResultObject = user
                };
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving user details for User ID = {UserID}", userModel.ID);
            return new Result("An error occurred while saving user details", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
        }
    }
    public async Task<Result> GetAllUserDetailsAsync(Pagination pagination)

    {
        try
        {
            _logger.LogDebug("Starting to fetch all user details with pagination parameters: {Pagination}", pagination);

            List<UserDetailsList> userData = (await _userRepository.GetAllUserDetailsAsync(pagination)).ToList();

            _logger.LogDebug("Successfully fetched {UserCount} user details.", userData.Count);

            return new Result
            {
                Status = StatusType.Success,
                HttpStatusCode = HttpStatusCode.OK,
                Message = "Get All User Details retrieved successfully.",
                ResultObject = userData
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all user details. Pagination: {Pagination}", pagination);
            return new Result("An error occurred while fetching all accounts, ERROR: " + ex.Message, "Accounts.UnknownError", HttpStatusCode.InternalServerError, ex);
        }
    }

    public async Task<Result> DeleteUserAsync(DeleteUserDto userModel)
    {
        try
        {
            _logger.LogDebug("Starting to delete user with ID: {UserID}", userModel.ID);

            Users? user = null;
            var userId = _commonService.GetLoggedUserId();

            if (IsUserLoggedInUser() && userModel.ID > 0 && userId == userModel.ID)
            {
                _logger.LogWarning("You are not allowed to delete your own data. User ID = {UserID}", userModel.ID);
                return new Result("You are not allowed to delete your own data", "Users.UserCanNotDeleteOwnData", HttpStatusCode.BadRequest);
            }
            else
            {
                if (userModel.ID > 0)
                {
                    user = await _userRepository.GetUserDetailByIDAsync(userModel.ID);

                    if (user == null)
                    {
                        _logger.LogWarning("User with ID {UserID} not found.", userModel.ID);
                        return new Result("User not exists", "Users.UserNotExists", HttpStatusCode.BadRequest);
                    }

                    _logger.LogInformation("User with ID {UserID} found. Proceeding to delete.", userModel.ID);
                    user.DeletedBy = userId;
                }

                user = await _userRepository.DeleteUserAsync(user);

                _logger.LogInformation("User with ID {UserID} deleted successfully.", userModel.ID);

                return new Result
                {
                    Status = StatusType.Success,
                    HttpStatusCode = HttpStatusCode.OK,
                    Message = "User deleted successfully.",
                    ResultObject = null
                };
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting user with ID: {UserID}", userModel.ID);
            return new Result("An error occurred while deleting user detail", "Users.UnknownError", HttpStatusCode.InternalServerError, ex);
        }
    }

    private string GenerateToken(Users userLoginDetail)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userLoginDetail.ID.ToString()), // Include User ID
            new Claim(ClaimTypes.Name, userLoginDetail.EmailID.ToString()),// Include Username
            new Claim("isPermission", userLoginDetail.Permissions.ToString())
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool IsUserLoggedInUser()
    {
        var userId = _commonService.GetLoggedUserId();
        bool isUserEditPermission = _commonService.GetLoggedUserPermission();

        return (userId > 0 && isUserEditPermission);
    }


}
