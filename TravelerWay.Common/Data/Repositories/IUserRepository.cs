using TravelerWay.Common.Entities;

namespace TravelerWay.Common.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetUserByUsernameAsync(string username);
    }
}