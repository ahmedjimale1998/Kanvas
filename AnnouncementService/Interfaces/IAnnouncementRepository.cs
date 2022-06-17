using AnnouncementService.Models;

namespace AnnouncementService.Interfaces
{ 
    public interface IAnnouncementRepository
    {
        Task<Announcement> Add(Announcement user);
        Task<Announcement> GetById(Guid id);
        Task<List<Announcement>> GetAll();
        Task<List<Announcement>> GetAllAnnouncementByClassId(int id);
        Task Update(Announcement user);
        Task Delete(Guid user);

    }
}
