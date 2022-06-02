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

        public async Task NewUserCreated(User user)
        {
            var welcomeMail = GetEmailBody(user);
            await Task.FromResult(context.Mail.Add(welcomeMail));
            context.SaveChanges();
        }

        private string GetWelcomeMail(User user)
        {
            return $"Welkom beste {user.Name}. " + System.Environment.NewLine +
                " Middels dit bericht willen wij u verwelkomen op de opleiding" + System.Environment.NewLine +
                "Met vriendelijke groet," + System.Environment.NewLine +
                "Het bestuur";
        }

        private Mail GetEmailBody(User user)
        {
            return new Mail(Guid.NewGuid(), "d870d3b3-c800-4427-9bf1-7ad3452f507d", user.Id.ToString(), "Welkomstberict van het Bestuur", GetWelcomeMail(user),DateTime.Now.ToUniversalTime());
        }
    }
}
