using UserService.DTOs;

namespace UserService.SyncDataServices.Http
{
    public interface IMailDataClient
    {
        Task SendUserToMail(UserReadDto user);
    }
}
