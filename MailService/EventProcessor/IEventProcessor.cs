namespace MailService.EventProcessor
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
