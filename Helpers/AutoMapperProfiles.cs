using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        //This class would be used by automapper as a profile for source and destination
        //Automapper is convention based so properties would be matched by naming convantion only, nothing more to do...
        //Only configuration is needed is because not matching properties
        public AutoMapperProfiles()
        {
            //mappings between source classes and destination dtos

            //CreateMap<User, UserForListDto>();
            //Setting an individual mapping so we can use URL on photos (ForMember allows to include diff mappings on an object)
            //For the age, there is no method on C# libraries to calculate age so need to create one for it
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(prop => prop.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            //CreateMap<User, UserForDetailedDto>();
            //Setting an individual mapping so we can use URL on photos
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.PhotoUrl,opt => opt.MapFrom(src => src.Photos.FirstOrDefault(prop => prop.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotosForDetailedDto>();

            CreateMap<UserForUpdateDto, User>();
        }
    }
}
