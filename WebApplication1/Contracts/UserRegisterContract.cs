namespace WebApplication1.Contracts
{
    public class UserRegisterContract
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

        public string PhotoPath { get; set; }

        public int Age { get; set; }
    }
}
