using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetByEmail(string email);
        User GetById(int id);
        bool EmailExists(string email);
        void AddUser(User user);
        void SaveChanges();
    }
}
