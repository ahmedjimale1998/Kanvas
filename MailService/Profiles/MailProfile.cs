using AutoMapper;
using MailService.DTOs;
using MailService.Models;

namespace MailService.Profiles
{
    public class MailProfile : Profile
    {
        public MailProfile()
        {
            CreateMap<MailCreatDto, Mail>();
            CreateMap<Mail, MailReadDto>();
            CreateMap<UserPublishedDto, User>();
        }
        

    }
}
