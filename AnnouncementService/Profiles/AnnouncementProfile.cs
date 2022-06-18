using AnnouncementService.DTOs;
using AnnouncementService.Models;
using AutoMapper;

namespace AnnouncementService
{
    public class AnnouncementProfile : Profile
    {
        public AnnouncementProfile()
        {
            CreateMap<AnnouncementCreateDto, Announcement>();
            CreateMap<AnnouncementReadDto, Announcement>();
            CreateMap<Announcement, AnnouncementReadDto>();
        }
       
    }
}
