using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user) => _context.Users.Add(user);

        public bool EmailExists(string email) => _context.Users.Any(u => u.Email == email);

        public List<User> GetAllUsers() => _context.Users.ToList();

        public User GetByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);

        public User GetById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
