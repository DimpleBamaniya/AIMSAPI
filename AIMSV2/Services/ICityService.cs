namespace Services;
public interface ICityService
{
    Task<Result> GetAllCitiesAsync();
}
