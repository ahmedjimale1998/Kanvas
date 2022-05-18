using MailService.Data;
using MailService.Interfaces;
using MailService.Models;

namespace MailService.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly MailContext context ;

        public MailRepository(MailContext Context)
        {
            this.context = Context;
        }

        public async Task<Mail> Add(Mail mail)
        {
            var connection = context.Database.CanConnect();
            context.Mail.Add(mail);
            context.SaveChanges();
            return await Get(mail.Id);
        }

        public async Task Delete(Guid id)
        {
            var mail = await Get(id);
            context.Mail.Remove(mail);
            context.SaveChanges();
        }

        public async Task<Mail> Get(Guid id)
        {
            var mail = await Task.FromResult(context.Mail.Find(id));
            if(mail != null)
            {
                return mail;
            }
            return new Mail();
        }

        public async Task<List<Mail>> GetAll()
        {
            var result = await Task.FromResult(context.Mail.ToList());
            return result;
        }

    }
}
