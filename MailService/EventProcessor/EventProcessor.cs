using AutoMapper;
using MailService.DTOs;
using MailService.Interfaces;
using MailService.Models;
using System.Text.Json;

namespace MailService.EventProcessor
{
    public class EventProcessor : IEventProcessor
    {
        public IServiceScopeFactory _scopeFactory { get; }

        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;   
        }
       
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.UserPublished:
                    addUser(message);
                    break;
                default:
                    break;

            }
        }

        private void addUser(string userPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IMailRepository>();
                
                var userPublishedDto = JsonSerializer.Deserialize<UserPublishedDto>(userPublishedMessage);

                try
                {
                    var user = _mapper.Map<User>(userPublishedDto);
                    if(user.Id.ToString() != null)
                    {
                        repo.NewUserCreated(user);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch(eventType.Event)
            {
                case "User_Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.UserPublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermind;
            }
        }
    }
    enum EventType
    {
        UserPublished,
        Undetermind
    }
}
