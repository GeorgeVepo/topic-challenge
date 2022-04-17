using topic_challenge.Data;
using topic_challenge.Models;
using topic_challenge.Repositories.Interfaces;

namespace topic_challenge.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SQLServerContext _sqlServerContext;
        public UserRepository(SQLServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }
        public void Create(User user)
        {
            _sqlServerContext.Add(user);
            _sqlServerContext.SaveChanges();
        }

        public User Find(int id)
        {
            return _sqlServerContext.Users.Find(id);
        }

        public User Login(string username, string password)
        {
            return _sqlServerContext.Users.Where(u => u.Name == username && u.Password == password).FirstOrDefault();
        }

        public void Update(User user)
        {
            _sqlServerContext.Update(user);
            _sqlServerContext.SaveChanges();
        }
    }
}
