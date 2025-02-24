namespace Api;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        AllowNullCollections = true;
        AllowNullDestinationValues = true;
        CreateMap<Users, UserDetails>().ReverseMap();
        CreateMap<UserDetails, Users>().ReverseMap();
    }
}
