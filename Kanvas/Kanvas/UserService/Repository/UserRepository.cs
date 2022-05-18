using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<User> Add(User user)
        {
            await Task.FromResult(context.User.Add(user));
            var addedUser = await Get(user.Id);
            var uses = await GetAllUsers();
            context.SaveChanges();
            return addedUser;
        }

        public async Task Delete(Guid guid)
        {
            var user = await Get(guid);
            await Task.FromResult(context.User.Remove(user));
        }

        public async Task<User> Get(Guid id)
        {
            var user = await Task.FromResult(context.User.Find(id));
            if (user != null)
            {
                return user;
            }
            return new User();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var aa = context.Database.CanConnect();
            var result = await Task.FromResult(context.User.ToList());
            return result;
        }

        public async Task Update(User user)
        {
            await Task.FromResult(context.User.Add(user));
        }

        public Task<List<User>> GetAllUsersByClassId(int id)
        {
            throw new NotImplementedException();
        }
        // why use dispose

        /* private bool _disposed;

         protected virtual void Dispose(bool disposing)
         {
             if (!_disposed)
             {
                 if (disposing)
                 {
                     _context.Dispose();
                 }
             }
             _disposed = true;
         }

         public void Dispose()
         {
             Dispose(true);
             GC.SuppressFinalize(this);
         }*/
    }
}
