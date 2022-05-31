using UserService.DTOs;

namespace UserService.SyncDataServices.Http
{
    public interface IMailDataClient
    {
        Task SendUserToCommand(UserReadDto user);
    }
}
