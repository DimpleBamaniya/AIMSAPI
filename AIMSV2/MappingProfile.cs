﻿using AutoMapper;

namespace AIMSAPI.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullCollections = true;
            AllowNullDestinationValues = true;

            //CreateMap<UserDetails, Users>().ReverseMap();
            //CreateMap<UserDto, Users>().ReverseMap();
        }
    }
}
