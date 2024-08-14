using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public User(string email, string password, Role role)
        {
            Guid = Guid.NewGuid();
            Email = email;
            Password = password;
            Role = role;
        }

        public Guid Guid { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
