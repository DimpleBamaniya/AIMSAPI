namespace AIMSV2.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<Entities.Cities>> GetAllCities();
    }
}
