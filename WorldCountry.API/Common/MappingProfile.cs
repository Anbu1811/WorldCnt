﻿using AutoMapper;
using WorldCountry.API.DTO;
using WorldCountry.API.Model;

namespace WorldCountry.API.Common
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // source Destination
            CreateMap<CreateCountryDTO, Country>().ReverseMap();
            CreateMap<Country, ShowCountryDTO>().ReverseMap();
            CreateMap<UpdateCountryDTO, Country>().ReverseMap();

        }
    }
}