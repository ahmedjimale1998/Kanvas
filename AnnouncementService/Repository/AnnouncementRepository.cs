using AnnouncementService.Data;
using AnnouncementService.Interfaces;
using AnnouncementService.Models;

namespace AnnouncementService.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AnnouncementContext _context;

        public AnnouncementRepository(AnnouncementContext context)
        {
            _context = context; 
        }

        public async Task<Announcement> Add(Announcement announcement)
        {
            _context.Announcement.Add(announcement);
            _context.SaveChanges();
            return await GetById(announcement.Id);
        }

        public async Task Delete(Guid id)
        {
            var announcement = await GetById(id);
            await Task.FromResult(_context.Announcement.Remove(announcement));
            _context.SaveChanges();
        }

        public async Task<List<Announcement>> GetAll()
        {
            var result = await Task.FromResult(_context.Announcement.ToList());
            return result;
        }

        public  async Task<List<Announcement>> GetAllAnnouncementByClassId(int id)
        {
            var results = await Task.FromResult(_context.Announcement.Where(x => x.ClassId == id).ToList());
            return results;
        }

        public async Task<Announcement> GetById(Guid id)
        {
            var announcement = await Task.FromResult(_context.Announcement.Find(id));
            if (announcement != null)
            {
                return announcement;
            }
            return new Announcement();
        }

        public async Task Update(Announcement announcement)
        {
            await Task.FromResult(_context.Announcement.Add(announcement));
            _context.SaveChanges();
        }
    }
}
