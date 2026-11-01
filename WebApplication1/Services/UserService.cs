using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class UserService(IUserRepository _userRepository)
    {
        public List<User> GetAllUsers() => _userRepository.GetAllUsers();

        public User GetUserByEmail(string email) => _userRepository.GetByEmail(email);

        public User GetUserById(int id) => _userRepository.GetById(id);

        public bool EmailExists(string email) => _userRepository.EmailExists(email);

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
            _userRepository.SaveChanges();
        }
    }
}
