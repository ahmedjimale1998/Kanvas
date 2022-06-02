using AutoMapper;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Source -> Target
            CreateMap<User,UserReadDto>();
            CreateMap<UserCreatDto, User>();
            CreateMap<UserReadDto,UserPublishedDto>();
            
        }
    }
}
