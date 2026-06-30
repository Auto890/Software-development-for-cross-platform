using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Services
{
    public class UserDbService : IUserService
    {
        private readonly UserDbContext _context;

        public UserDbService(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool Update(int id, User user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existing is null) return false;

            existing.Username = user.Username;
            existing.Fullname = user.Fullname;
            existing.Email = user.Email;
            existing.Role = user.Role;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existing is null) return false;

            _context.Users.Remove(existing);
            _context.SaveChanges();
            return true;
        }
    }
}