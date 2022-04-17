using topic_challenge.Models;

namespace topic_challenge.Services.Interfaces
{
    public interface IUserService
    {
        User Login(string username, string password);
        User Find(int id);
        void Create(User user);
        void Update(User user);
    }
}
