
namespace Services
{
    public class CommonService : ICommonService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetLoggedUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || string.IsNullOrEmpty(userId))
            {
                throw new NotImplementedException("User ID not found in token");
            }
            int loggedUserId = int.Parse(userId);
            return loggedUserId;
        }
        public bool GetLoggedUserPermission()
        {
            var permissionValue = _httpContextAccessor.HttpContext?.User?.FindFirst("isPermission")?.Value;

            if (!string.IsNullOrEmpty(permissionValue) && bool.TryParse(permissionValue, out bool parsedPermission))
            {
                return parsedPermission; // Return the parsed boolean value
            }

            throw new InvalidOperationException("Invalid or missing permission value in token");
        }
    }
}
