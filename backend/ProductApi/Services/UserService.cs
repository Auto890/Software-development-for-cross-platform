using ProductApi.Models;

namespace ProductApi.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Create(User user);
        bool Update(int id, User user);
        bool Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users = new()
        {
            new User { Id = 1, Username = "somchai_it", Fullname = "Somchai Jaidee", Email = "somchai@email.com", Role = "Admin" },
            new User { Id = 2, Username = "somsri_dev", Fullname = "Somsri Ping", Email = "somsri@email.com", Role = "User" }
        };

        private int _nextId = 3;

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public User Create(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, User user)
        {
            var existing = GetById(id);
            if (existing is null) return false;

            existing.Username = user.Username;
            existing.Fullname = user.Fullname;
            existing.Email = user.Email;
            existing.Role = user.Role;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing is null) return false;
            _users.Remove(existing);
            return true;
        }
    }
}