using UserService.DTOs;

namespace UserService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewUser(UserPublishedDto userPublishedDto);
    }
}
