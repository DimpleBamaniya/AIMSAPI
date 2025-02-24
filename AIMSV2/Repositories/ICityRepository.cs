namespace Repositories;
public interface ICityRepository
{
    Task<IEnumerable<Cities>> GetAllCitiesAsync();
}
