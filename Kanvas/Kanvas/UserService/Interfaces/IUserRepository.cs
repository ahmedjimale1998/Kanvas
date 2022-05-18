using UserService.Models;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> Get(Guid id);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetAllUsersByClassId(int id);
        Task Update(User user);
        Task Delete(Guid user);

    }
}
