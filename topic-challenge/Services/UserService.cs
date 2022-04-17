using topic_challenge.Models;
using topic_challenge.Repositories.Interfaces;
using topic_challenge.Services.Interfaces;

namespace topic_challenge.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public User Find(int id)
        {
           return _userRepository.Find(id);
        }

        public User? Login(string username, string password)
        {
            return _userRepository.Login(username, password);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }
    }
}
