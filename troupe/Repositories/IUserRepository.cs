using troupe.Models;

namespace troupe.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
    }
}