using AIMSV2.Models;
using AutoMapper;
using Entities;

namespace AIMSAPI.Api
{
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
}
