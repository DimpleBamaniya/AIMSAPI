namespace Services
{
    public interface ICommonService
    {
        public int GetLoggedUserId();
        public bool GetLoggedUserPermission();
    }
}
