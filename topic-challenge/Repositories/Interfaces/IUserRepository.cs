using topic_challenge.Models;

namespace topic_challenge.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? Login(string username, string password);
        void Create(User user);
        void Update(User user);
        User Find(int id);
    }
}
