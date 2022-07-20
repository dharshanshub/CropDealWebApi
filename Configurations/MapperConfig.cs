﻿using AutoMapper;
using CropDealWebAPI.Dtos.UserProfile;
using CropDealWebAPI.Models;

namespace CropDealWebAPI.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<CreateUserDto, UserProfile>().ReverseMap();
            CreateMap<UpdateUserDto, UserProfile>().ReverseMap();
            CreateMap<GetUserDto, UserProfile>().ReverseMap();
        }
    }
}
