using MailService.Models;

namespace MailService.Interfaces
{
    public interface IMailRepository
    {
        Task<Mail> Add(Mail mail);
        Task<Mail> Get(Guid id);
        Task<List<Mail>> GetAll();
        Task Delete(Guid id);
    }
}
